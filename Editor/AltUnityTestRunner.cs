using System.Linq;

namespace JustUnityTester.Editor {
    public delegate void TestRunDelegate(string name);

    public class AltUnityTestRunner {
        public enum TestRunMode { RunAllTest, RunSelectedTest, RunFailedTest }

        private static System.Threading.Thread thread;
        //This are for progressBar when are runned
        private static float progress;
        private static float total;
        private static string _testName;

        public static TestRunDelegate CallRunDelegate = new TestRunDelegate(ShowProgresBar);


        public static void RunTests(TestRunMode testMode) {
            UnityEngine.Debug.Log("Started running test");
            System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            System.Reflection.Assembly assembly = assemblies.FirstOrDefault(assemblyName => assemblyName.GetName().Name.Equals("Assembly-CSharp-Editor"));

            var filters = AddTestToBeRun(testMode);
            NUnit.Framework.Interfaces.ITestListener listener = new AltUnityTestRunListener(CallRunDelegate);
            var testAssemblyRunner = new NUnit.Framework.Api.NUnitTestAssemblyRunner(new NUnit.Framework.Api.DefaultTestAssemblyBuilder());

            testAssemblyRunner.Load(assembly, new System.Collections.Generic.Dictionary<string, object>());
            progress = 0;
            total = filters.Filters.Count;
            System.Threading.Thread runTestThread = new System.Threading.Thread(() => {
                var result = testAssemblyRunner.Run(listener, filters);
                SetTestStatus(result);
                TesterEditor.isTestRunResultAvailable = true;
                TesterEditor.selectedTest = -1;
            });

            runTestThread.Start();
            if (TesterEditor.Config.platform != TestPlatform.Editor) {
                float previousProgres = progress - 1;
                while (runTestThread.IsAlive) {
                    if (previousProgres == progress) continue;
                    UnityEditor.EditorUtility.DisplayProgressBar(progress == total ? "This may take a few seconds" : _testName,
                        progress + "/" + total, progress / total);
                    previousProgres = progress;
                }
            }

            runTestThread.Join();
            if (TesterEditor.Config.platform != TestPlatform.Editor) {
                TesterEditor.needsRepaiting = true;
                UnityEditor.EditorUtility.ClearProgressBar();
            }
        }



        private static void ShowProgresBar(string name) {
            progress++;
            _testName = name;
        }

        private void SetTestStatus(System.Collections.Generic.List<NUnit.Framework.Interfaces.ITestResult> results) {
            bool passed = true;
            int numberOfTestPassed = 0;
            int numberOfTestFailed = 0;
            double totalTime = 0;
            foreach (var test in TesterEditor.Config.MyTests) {
                int counter = 0;
                // int testPassed = 0;
                int testPassedCounter = 0;
                int testFailedCounter = 0;
                foreach (var result in results) {
                    if (test.Type == typeof(NUnit.Framework.Internal.TestAssembly)) {
                        counter++;
                        var enumerator = result.Children.GetEnumerator();
                        enumerator.MoveNext();
                        if (enumerator.Current != null) {
                            var enumerator2 = enumerator.Current.Children.GetEnumerator();
                            enumerator2.MoveNext();
                            if (enumerator2.Current != null && enumerator2.Current.FailCount > 0) {

                                testFailedCounter++;
                            } else if (enumerator2.Current != null && enumerator2.Current.PassCount > 0) {
                                testPassedCounter++;
                            }

                            enumerator2.Dispose();
                        }

                        enumerator.Dispose();

                    }

                    if (test.Type == typeof(NUnit.Framework.Internal.TestFixture)) {
                        var enumerator = result.Children.GetEnumerator();
                        enumerator.MoveNext();
                        if (enumerator.Current != null && enumerator.Current.FullName.Equals(test.TestName)) {
                            counter++;
                            var enumerator2 = enumerator.Current.Children.GetEnumerator();
                            enumerator2.MoveNext();
                            if (enumerator2.Current != null && enumerator2.Current.FailCount > 0) {
                                testFailedCounter++;

                            } else if (enumerator2.Current != null && enumerator2.Current.PassCount > 0) {
                                testPassedCounter++;

                            }
                            enumerator2.Dispose();
                        }
                        enumerator.Dispose();
                    }

                    if (test.Type == typeof(NUnit.Framework.Internal.TestMethod)) {
                        var enumerator = result.Children.GetEnumerator();
                        enumerator.MoveNext();
                        if (enumerator.Current != null) {
                            var enumerator2 = enumerator.Current.Children.GetEnumerator();
                            enumerator2.MoveNext();
                            if (enumerator2.Current != null && enumerator2.Current.FullName.Equals(test.TestName)) {
                                if (enumerator2.Current.FailCount > 0) {
                                    test.Status = -1;
                                    test.TestResultMessage = enumerator2.Current.Message + " \n\n\n StackTrace:  " + enumerator2.Current.StackTrace;
                                    passed = false;
                                    numberOfTestFailed++;

                                } else if (enumerator2.Current.PassCount > 0) {
                                    test.Status = 1;
                                    test.TestResultMessage = "Passed in " + enumerator2.Current.Duration;
                                    numberOfTestPassed++;
                                }

                                totalTime += (enumerator2.Current.EndTime - enumerator2.Current.StartTime).TotalSeconds;
                            }
                            enumerator2.Dispose();
                        }

                        enumerator.Dispose();
                    }


                }

                if (test.Type != typeof(NUnit.Framework.Internal.TestMethod)) {
                    if (test.TestCaseCount == counter) {
                        if (testFailedCounter == 0 && testPassedCounter == counter) {
                            test.Status = 1;
                            test.TestResultMessage = "All method passed ";
                        } else {
                            test.Status = -1;
                            passed = false;
                            test.TestResultMessage = "There are methods that failed";
                        }
                    }
                }
            }
            var listOfTests = TesterEditor.Config.MyTests;
            var serializeTests = Newtonsoft.Json.JsonConvert.SerializeObject(listOfTests, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            UnityEditor.EditorPrefs.SetString("tests", serializeTests);

            TesterEditor.reportTestPassed = numberOfTestPassed;
            TesterEditor.reportTestFailed = numberOfTestFailed;
            TesterEditor.isTestRunResultAvailable = true;
            TesterEditor.selectedTest = -1;
            TesterEditor.timeTestRan = totalTime;
            if (passed) {
                UnityEngine.Debug.Log("All test passed");
            } else
                UnityEngine.Debug.Log("Test failed");
        }

        private static NUnit.Framework.Internal.Filters.OrFilter AddTestToBeRun(TestRunMode testMode) {
            NUnit.Framework.Internal.Filters.OrFilter filter = new NUnit.Framework.Internal.Filters.OrFilter();
            switch (testMode) {
                case TestRunMode.RunAllTest:
                    foreach (var test in TesterEditor.Config.MyTests)
                        if (!test.IsSuite)
                            filter.Add(new NUnit.Framework.Internal.Filters.FullNameFilter(test.TestName));
                    break;
                case TestRunMode.RunSelectedTest:
                    foreach (var test in TesterEditor.Config.MyTests)
                        if (test.Selected && !test.IsSuite)
                            filter.Add(new NUnit.Framework.Internal.Filters.FullNameFilter(test.TestName));
                    break;
                case TestRunMode.RunFailedTest:
                    foreach (var test in TesterEditor.Config.MyTests)
                        if (test.Status == -1 && !test.IsSuite)
                            filter.Add(new NUnit.Framework.Internal.Filters.FullNameFilter(test.TestName));
                    break;
            }

            return filter;
        }

        static int SetTestStatus(NUnit.Framework.Interfaces.ITestResult test) {

            if (!test.Test.IsSuite) {
                var status = 0;
                if (test.PassCount == 1) {
                    status = 1;
                    TesterEditor.reportTestPassed++;
                } else if (test.FailCount == 1) {
                    status = -1;
                    TesterEditor.reportTestFailed++;
                }
                TesterEditor.timeTestRan += test.Duration;
                int index = TesterEditor.Config.MyTests.FindIndex(a => a.TestName.Equals(test.Test.FullName));
                TesterEditor.Config.MyTests[index].Status = status;
                TesterEditor.Config.MyTests[index].TestDuration = test.Duration;
                TesterEditor.Config.MyTests[index].TestStackTrace = test.StackTrace;
                TesterEditor.Config.MyTests[index].TestResultMessage = test.Message;
                return status;
            }

            var failCount = 0;
            var notExecutedCount = 0;
            var passCount = 0;
            foreach (var testChild in test.Children) {
                var status = SetTestStatus(testChild);
                if (status == 0)
                    notExecutedCount++;
                else if (status == -1) {
                    failCount++;

                } else {
                    passCount++;
                }
            }

            if (test.Test.TestCaseCount != passCount + failCount + notExecutedCount) {
                TesterEditor.Config.MyTests[TesterEditor.Config.MyTests.FindIndex(a => a.TestName.Equals(test.Test.FullName))].Status = 0;
                return 0;
            }

            if (failCount > 0) {
                TesterEditor.Config.MyTests[TesterEditor.Config.MyTests.FindIndex(a => a.TestName.Equals(test.Test.FullName))].Status = -1;
                return -1;

            }
            TesterEditor.Config.MyTests[TesterEditor.Config.MyTests.FindIndex(a => a.TestName.Equals(test.Test.FullName))].Status = 1;
            return 1;
        }

        public static void SetUpListTest() {
            var reload = TesterEditor.Config.MyTests == null;
            if (!reload) {
                foreach (var test in TesterEditor.Config.MyTests) {
                    if (test.Type == null) {
                        reload = true;
                    }
                }
            }
            if (reload == false) {
                return;
            }

            var myTests = new System.Collections.Generic.List<MyTest>();
            System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            const string engineTestRunnerAssemblyName = "UnityEngine.TestRunner";
            const string editorTestRunnerAssemblyName = "UnityEditor.TestRunner";
            const string editorAssemblyName = "Assembly-CSharp-Editor";
            foreach (var assembly in assemblies) {
                /*
                 * Skips test runner assemblies and assemblies that do not contain references to test assemblies
                 */
                bool isEditorAssembly = assembly.GetName().Name.Equals(editorAssemblyName);
                if (!isEditorAssembly) {
                    bool isEngineTestRunnerAssembly = assembly.GetName().Name.Contains(engineTestRunnerAssemblyName);
                    bool isEditorTestRunnerAssembly = assembly.GetName().Name.Contains(editorTestRunnerAssemblyName);
                    bool isTestAssembly = assembly.GetReferencedAssemblies().FirstOrDefault(
                                reference => reference.Name.Contains(engineTestRunnerAssemblyName)
                                             || reference.Name.Contains(editorTestRunnerAssemblyName)) == null;
                    if (isEngineTestRunnerAssembly ||
                        isEditorTestRunnerAssembly ||
                        isTestAssembly) {
                        continue;
                    }
                }

                var testSuite = (NUnit.Framework.Internal.TestSuite)new NUnit.Framework.Api.DefaultTestAssemblyBuilder().Build(assembly, new System.Collections.Generic.Dictionary<string, object>());
                addTestSuiteToMyTest(testSuite, myTests);
            }
            SetCorrectCheck(myTests);
            TesterEditor.Config.MyTests = myTests;
        }

        private static void SetCorrectCheck(System.Collections.Generic.List<MyTest> myTests) {
            bool classCheck = true;
            bool assemblyCheck = true;
            for (int i = myTests.Count - 1; i >= 0; i--) {
                MyTest test = myTests[i];
                System.Type testType = test.Type;
                switch (testType.ToString()) {
                    case "NUnit.Framework.Internal.TestMethod":
                        if (!test.Selected)//test not selected then the class which the test belong must be not selected
                        {
                            classCheck = false;
                        }
                        break;
                    case "NUnit.Framework.Internal.TestFixture":
                        if (classCheck) {
                            test.Selected = true;
                        } else {
                            test.Selected = false;
                            assemblyCheck = false;//class not selected then the assembly which the test belong must be not selected
                        }
                        classCheck = true;//Reset value for new class
                        break;
                    case "NUnit.Framework.Internal.TestAssembly":
                        if (assemblyCheck) {
                            test.Selected = true;
                        } else {
                            test.Selected = false;
                        }
                        assemblyCheck = true;//Reset value for new assembly
                        break;
                }
            }
        }

        private static void addTestSuiteToMyTest(NUnit.Framework.Interfaces.ITest testSuite, System.Collections.Generic.List<MyTest> newMyTests) {
            string path = null;

            if (testSuite.GetType() == typeof(NUnit.Framework.Internal.TestMethod)) {
                var fullName = testSuite.FullName;
                var className = fullName.Split('.')[0];
                var assets = UnityEditor.AssetDatabase.FindAssets(className);
                if (assets.Length != 0) {
                    path = UnityEditor.AssetDatabase.GUIDToAssetPath(assets[0]);
                }
            }
            var parentName = testSuite.Parent?.FullName ?? string.Empty;

            MyTest index = null;
            if (TesterEditor.Config.MyTests != null)
                index = TesterEditor.Config.MyTests.FirstOrDefault(a => a.TestName.Equals(testSuite.FullName) && a.ParentName.Equals(parentName));

            if (index == null) {
                newMyTests.Add(new MyTest(false, testSuite.FullName, 0, testSuite.IsSuite, testSuite.GetType(),
                    parentName, testSuite.TestCaseCount, false, null, null, 0, path));
            } else {
                newMyTests.Add(new MyTest(index.Selected, index.TestName, index.Status, index.IsSuite, testSuite.GetType(),
                   index.ParentName, testSuite.TestCaseCount, index.FoldOut, index.TestResultMessage, index.TestStackTrace, index.TestDuration, path));
            }


            foreach (var test in testSuite.Tests) {
                addTestSuiteToMyTest(test, newMyTests);
            }
        }

        static void RunTestFromCommandLine() {

            var arguments = System.Environment.GetCommandLineArgs();

            bool runAllTests = true;
            var testAssemblyRunner = new NUnit.Framework.Api.NUnitTestAssemblyRunner(new NUnit.Framework.Api.DefaultTestAssemblyBuilder());
            NUnit.Framework.Internal.TestSuite testSuite = null;
            NUnit.Framework.Internal.Filters.OrFilter filter = new NUnit.Framework.Internal.Filters.OrFilter();
            NUnit.Framework.Interfaces.ITestListener listener = new AltUnityTestRunListener(null);
            System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            System.Reflection.Assembly assembly = assemblies.FirstOrDefault(assemblyName => assemblyName.GetName().Name.Equals("Assembly-CSharp-Editor"));
            testAssemblyRunner.Load(assembly, new System.Collections.Generic.Dictionary<string, object>());

            foreach (var arg in arguments) {
                if (arg.Equals("-testsClass") || arg.Equals("-tests")) {
                    runAllTests = false;
                    break;
                }
            }
            TesterEditor.InitEditorConfiguration();
            var tests = TesterEditor.Config.MyTests;

            if (!runAllTests) {
                System.Collections.Generic.List<string> ClassToTest = new System.Collections.Generic.List<string>();
                System.Collections.Generic.List<string> Tests = new System.Collections.Generic.List<string>();
                int argumentFound = 0;
                for (int i = 0; i < arguments.Length; i++) {
                    if (argumentFound != 0) {
                        if (arguments[i].StartsWith("-")) {
                            argumentFound = 0;
                        } else {
                            switch (argumentFound) {
                                case 1:
                                    ClassToTest.Add(arguments[i]);
                                    break;
                                case 2:
                                    Tests.Add(arguments[i]);
                                    break;
                            }
                        }
                    }
                    if (arguments[i].Equals("-testsClass")) {
                        argumentFound = 1;
                        continue;
                    }
                    if (arguments[i].Equals("-tests")) {
                        argumentFound = 2;
                        continue;
                    }
                }
                foreach (var className in ClassToTest) {
                    var classIndex = tests.FindIndex(test => test.TestName.Equals(className));
                    if (classIndex != -1) {
                        var classFoundInList = tests[classIndex];
                        for (int i = 0; i < classFoundInList.TestCaseCount; i++) {
                            filter.Add(new NUnit.Framework.Internal.Filters.FullNameFilter(tests[i + classIndex + 1].TestName));
                        }
                    } else {
                        throw new System.Exception("Class name: " + className + " not found");
                    }

                }
                foreach (var testName in Tests) {
                    var classIndex = tests.FindIndex(test => test.TestName.Equals(testName));
                    if (classIndex != -1) {
                        filter.Add(new NUnit.Framework.Internal.Filters.FullNameFilter(tests[classIndex].TestName));
                    } else {
                        throw new System.Exception("Test name: " + testName + " not found");
                    }

                }

            } else //NoArgumentsGiven
              {

                testSuite = (NUnit.Framework.Internal.TestSuite)new NUnit.Framework.Api.DefaultTestAssemblyBuilder().Build(assembly, new System.Collections.Generic.Dictionary<string, object>());
                foreach (var test in testSuite.Tests)
                    foreach (var t in test.Tests) {
                        UnityEngine.Debug.Log(t.FullName);
                        filter.Add(new NUnit.Framework.Internal.Filters.FullNameFilter(t.FullName));
                    }
            }
            var result = testAssemblyRunner.Run(listener, filter);
            if (result.FailCount > 0) {
                UnityEditor.EditorApplication.Exit(1);
            } else {
                UnityEditor.EditorApplication.Exit(0);
            }

        }


    }
}