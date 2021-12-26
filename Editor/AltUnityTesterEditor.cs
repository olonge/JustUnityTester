using System.Collections.Generic;
using System.Linq;

public class AltUnityTesterEditor : UnityEditor.EditorWindow
{
    private UnityEngine.UI.Button _android;
    UnityEngine.Object _obj;

    public static bool needsRepaiting = false;

    public static AltUnityEditorConfiguration EditorConfiguration;
    public static AltUnityTesterEditor _window;

    public static NUnit.Framework.Internal.TestSuite _testSuite;

    // public TestRunDelegate CallRunDelegateCommandline = new TestRunDelegate();

    private static UnityEngine.Texture2D passIcon;
    private static UnityEngine.Texture2D failIcon;
    private static UnityEngine.Texture2D downArrowIcon;
    private static UnityEngine.Texture2D upArrowIcon;
    private static UnityEngine.Texture2D infoIcon;
    private static UnityEngine.Texture2D openFileIcon;
    private static UnityEngine.Texture2D xIcon;
    private static UnityEngine.Texture2D reloadIcon;


    public static int selectedTest = -1;
    private static UnityEngine.Color defaultColor;
    private static UnityEngine.Color greenColor = new UnityEngine.Color(0.0f, 0.5f, 0.2f, 1f);
    private static UnityEngine.Color redColor = new UnityEngine.Color(0.7f, 0.15f, 0.15f, 1f);
    private static UnityEngine.Color selectedTestColor = new UnityEngine.Color(1f, 1f, 1f, 1f);
    private static UnityEngine.Color selectedTestColorDark = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
    private static UnityEngine.Color oddNumberTestColor = new UnityEngine.Color(0.75f, 0.75f, 0.75f, 1f);
    private static UnityEngine.Color evenNumberTestColor = new UnityEngine.Color(0.7f, 0.7f, 0.7f, 1f);
    private static UnityEngine.Color oddNumberTestColorDark = new UnityEngine.Color(0.23f, 0.23f, 0.23f, 1f);
    private static UnityEngine.Color evenNumberTestColorDark = new UnityEngine.Color(0.25f, 0.25f, 0.25f, 1f);
    private static UnityEngine.Texture2D selectedTestTexture;
    private static UnityEngine.Texture2D oddNumberTestTexture;
    private static UnityEngine.Texture2D evenNumberTestTexture;
    private static UnityEngine.Texture2D portForwardingTexture;


    private static long timeSinceLastClick;
    UnityEngine.Vector2 _scrollPosition;
    private UnityEngine.Vector2 _scrollPositonTestResult;


    private bool _foldOutScenes = true;
    private bool _foldOutBuildSettings = true;
    private bool _foldOutIosSettings = true;
    private bool _foldOutAltUnityServerSettings = true;

    //TestResult after running a test
    public static bool isTestRunResultAvailable = false;
    public static int reportTestPassed;
    public static int reportTestFailed;
    public static double timeTestRan;

    public static System.Collections.Generic.List<AltUnityMyDevices> devices = new System.Collections.Generic.List<AltUnityMyDevices>();
    // public static System.Collections.Generic.Dictionary<string, int> iosForwards = new System.Collections.Generic.Dictionary<string, int>();

    // Add menu item named "My Window" to the Window menu
    [UnityEditor.MenuItem("Window/Just Unity Tester/Inspector")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        _window = (AltUnityTesterEditor)GetWindow(typeof(AltUnityTesterEditor));
        _window.minSize = new UnityEngine.Vector2(600, 100);
        _window.Show();

    }


    private void OnFocus()
    {
        var color = UnityEngine.Color.black;
        if (UnityEditor.EditorGUIUtility.isProSkin)
        {
            color = UnityEngine.Color.white;
        }
        if (EditorConfiguration == null)
        {
            InitEditorConfiguration();
        }
        if (!UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources/AltUnityTester"))
        {
            AltUnityBuilder.CreateJsonFileForInputMappingOfAxis();
        }
        if (failIcon == null)
        {
            var findIcon = UnityEditor.AssetDatabase.FindAssets("16px-indicator-fail");
            failIcon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(findIcon[0]));
        }
        if (passIcon == null)
        {
            var findIcon = UnityEditor.AssetDatabase.FindAssets("16px-indicator-pass");
            passIcon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(findIcon[0]));

        }
        if (downArrowIcon == null)
        {
            var findIcon = UnityEditor.EditorGUIUtility.isProSkin ? UnityEditor.AssetDatabase.FindAssets("downArrowIcon") : UnityEditor.AssetDatabase.FindAssets("downArrowIconDark");
            downArrowIcon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(findIcon[0]));

        }
        if (upArrowIcon == null)
        {
            var findIcon = UnityEditor.EditorGUIUtility.isProSkin ? UnityEditor.AssetDatabase.FindAssets("upArrowIcon") : UnityEditor.AssetDatabase.FindAssets("upArrowIconDark");
            upArrowIcon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(findIcon[0]));

        }

        if (xIcon == null)
        {
            var findIcon = UnityEditor.EditorGUIUtility.isProSkin ? UnityEditor.AssetDatabase.FindAssets("xIcon") : UnityEditor.AssetDatabase.FindAssets("xIconDark");
            xIcon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(findIcon[0]));

        }
        if (reloadIcon == null)
        {
            var findIcon = UnityEditor.EditorGUIUtility.isProSkin ? UnityEditor.AssetDatabase.FindAssets("reloadIcon") : UnityEditor.AssetDatabase.FindAssets("reloadIconDark");
            reloadIcon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(findIcon[0]));

        }

        if (selectedTestTexture == null)
        {
            selectedTestTexture = MakeTexture(20, 20, UnityEditor.EditorGUIUtility.isProSkin ? selectedTestColorDark : selectedTestColor);
        }
        if (evenNumberTestTexture == null)
        {
            evenNumberTestTexture = MakeTexture(20, 20, UnityEditor.EditorGUIUtility.isProSkin ? evenNumberTestColorDark : evenNumberTestColor);
        }
        if (oddNumberTestTexture == null)
        {
            oddNumberTestTexture = MakeTexture(20, 20, UnityEditor.EditorGUIUtility.isProSkin ? oddNumberTestColorDark : oddNumberTestColor);
        }
        if (portForwardingTexture == null)
        {
            portForwardingTexture = MakeTexture(20, 20, greenColor);
        }

        GetListOfSceneFromEditor();
        AltUnityTestRunner.SetUpListTest();


    }

    private void GetListOfSceneFromEditor()
    {
        System.Collections.Generic.List<AltUnityMyScenes> newSceneses = new System.Collections.Generic.List<AltUnityMyScenes>();
        foreach (var scene in UnityEditor.EditorBuildSettings.scenes)
        {
            newSceneses.Add(new AltUnityMyScenes(scene.enabled, scene.path, 0));
        }

        EditorConfiguration.Scenes = newSceneses;
    }


    public static void InitEditorConfiguration()
    {
        if (UnityEditor.AssetDatabase.FindAssets("AltUnityTesterEditorSettings").Length == 0)
        {
            var altUnityEditorFolderPath = UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("AltUnityTesterEditor")[0]);
            altUnityEditorFolderPath = altUnityEditorFolderPath.Substring(0, altUnityEditorFolderPath.Length - 24);
            EditorConfiguration = UnityEngine.ScriptableObject.CreateInstance<AltUnityEditorConfiguration>();
            EditorConfiguration.MyTests=null;
            UnityEditor.AssetDatabase.CreateAsset(EditorConfiguration, altUnityEditorFolderPath + "/AltUnityTesterEditorSettings.asset");
            UnityEditor.AssetDatabase.SaveAssets();

        }
        else
        {
            EditorConfiguration = UnityEditor.AssetDatabase.LoadAssetAtPath<AltUnityEditorConfiguration>(
                UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("AltUnityTesterEditorSettings")[0]));
        }
        UnityEditor.EditorUtility.SetDirty(EditorConfiguration);
    }


    void OnInspectorUpdate()
    {
        Repaint();
        if (isTestRunResultAvailable) {
            isTestRunResultAvailable = !UnityEditor.EditorUtility.DisplayDialog("Test Report",
                  " Total tests:" + (reportTestFailed + reportTestPassed) + System.Environment.NewLine + " Tests passed:" +
                  reportTestPassed + System.Environment.NewLine + " Tests failed:" + reportTestFailed + System.Environment.NewLine +
                  " Duration:" + timeTestRan + " seconds", "Ok");
            reportTestFailed = 0;
            reportTestPassed = 0;
            timeTestRan = 0;
        }
    }

    private void OnGUI()
    {

        if (needsRepaiting)
        {
            needsRepaiting = false;
            Repaint();
        }

        if (UnityEngine.Application.isPlaying && !EditorConfiguration.ranInEditor)
        {
            EditorConfiguration.ranInEditor = true;
        }

        if (!UnityEngine.Application.isPlaying && EditorConfiguration.ranInEditor)
        {
            AfterExitPlayMode();

        }

        DrawGUI();

    }

    private void DrawGUI()
    {
        var screenWidth = UnityEditor.EditorGUIUtility.currentViewWidth;
        //----------------------Left Panel------------


        UnityEditor.EditorGUILayout.BeginHorizontal();
        var leftSide = (screenWidth / 3) * 2;
        _scrollPosition = UnityEditor.EditorGUILayout.BeginScrollView(_scrollPosition, false, false, UnityEngine.GUILayout.MinWidth(leftSide));
        if (EditorConfiguration.MyTests != null)
            DisplayTestGui(EditorConfiguration.MyTests);

        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.EndScrollView();

        //-------------------Right Panel--------------
        var rightSide = (screenWidth / 3);
        UnityEditor.EditorGUILayout.BeginVertical();

        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.Separator();

        
        if (AltUnityBuilder.built)
        {
            var found = false;

            UnityEngine.SceneManagement.Scene scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(AltUnityBuilder.GetFirstSceneWhichWillBeBuilt());
            if (scene.path.Equals(AltUnityBuilder.GetFirstSceneWhichWillBeBuilt()))
            {
                if (scene.GetRootGameObjects()
                    .Any(gameObject => gameObject.name.Equals("AltUnityRunnerPrefab")))
                {
                    UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
                    var altunityRunner = scene.GetRootGameObjects()
                        .First(a => a.name.Equals("AltUnityRunnerPrefab"));
                    DestroyAltUnityRunner(altunityRunner);
                    found = true;
                }

                if (found == false)
                    AltUnityBuilder.built = false;
            }

        }

        UnityEditor.EditorGUILayout.LabelField("Visuals", UnityEditor.EditorStyles.boldLabel);
        UnindentedLabelAndCheckboxHorizontalLayout("Input visualizer:", ref EditorConfiguration.inputVisualizer);
        UnindentedLabelAndCheckboxHorizontalLayout("Show popup", ref EditorConfiguration.showPopUp);

        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.Separator();

        UnityEditor.EditorGUILayout.LabelField("Run", UnityEditor.EditorStyles.boldLabel);
        if (EditorConfiguration.platform == AltUnityPlatform.Editor)
        {
            if (UnityEditor.EditorApplication.isPlaying) {
                UnityEditor.EditorGUI.BeginDisabledGroup(true);
                UnityEngine.GUILayout.Button("Play in Editor");
                UnityEditor.EditorGUI.EndDisabledGroup();
            } else if (UnityEngine.GUILayout.Button("Play in Editor")) {
                RunInEditor();
            }

            if (!UnityEditor.EditorApplication.isPlaying) {
                UnityEditor.EditorGUI.BeginDisabledGroup(true);
                UnityEngine.GUILayout.Button("End Tests");
                UnityEditor.EditorGUI.EndDisabledGroup();
            } else if (UnityEngine.GUILayout.Button("End Tests")) {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        else
        {
            UnityEditor.EditorGUI.BeginDisabledGroup(true);
            UnityEngine.GUILayout.Button("Play in Editor");
            UnityEditor.EditorGUI.EndDisabledGroup();
        }

        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.Separator();
        UnityEditor.EditorGUILayout.LabelField("Run tests", UnityEditor.EditorStyles.boldLabel);

        if (!UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorGUI.BeginDisabledGroup(true);
            UnityEngine.GUILayout.Button("Run All Tests");
            UnityEditor.EditorGUI.EndDisabledGroup();
        } else if (UnityEngine.GUILayout.Button("Run All Tests")) {
            if (EditorConfiguration.platform == AltUnityPlatform.Editor)
            {
                System.Threading.Thread testThread = new System.Threading.Thread(() => AltUnityTestRunner.RunTests(AltUnityTestRunner.TestRunMode.RunAllTest));
                testThread.Start();
            }
            else
            {

                AltUnityTestRunner.RunTests(AltUnityTestRunner.TestRunMode.RunAllTest);
            }
        }

        if (!UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorGUI.BeginDisabledGroup(true);
            UnityEngine.GUILayout.Button("Run Selected Tests");
            UnityEditor.EditorGUI.EndDisabledGroup();
        } else if (UnityEngine.GUILayout.Button("Run Selected Tests"))
        {
            if (EditorConfiguration.platform == AltUnityPlatform.Editor)
            {
                System.Threading.Thread testThread = new System.Threading.Thread(() => AltUnityTestRunner.RunTests(AltUnityTestRunner.TestRunMode.RunSelectedTest));
                testThread.Start();
            }
            else
            {

                AltUnityTestRunner.RunTests(AltUnityTestRunner.TestRunMode.RunSelectedTest);
            }
        }

        if (!UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorGUI.BeginDisabledGroup(true);
            UnityEngine.GUILayout.Button("Run Failed Tests");
            UnityEditor.EditorGUI.EndDisabledGroup();
        } else if (UnityEngine.GUILayout.Button("Run Failed Tests"))
        {
            if (EditorConfiguration.platform == AltUnityPlatform.Editor)
            {
                System.Threading.Thread testThread = new System.Threading.Thread(() => AltUnityTestRunner.RunTests(AltUnityTestRunner.TestRunMode.RunFailedTest));
                testThread.Start();
            }
            else
            {

                AltUnityTestRunner.RunTests(AltUnityTestRunner.TestRunMode.RunFailedTest);
            }
        }

        

        //Status test

        _scrollPositonTestResult = UnityEditor.EditorGUILayout.BeginScrollView(_scrollPositonTestResult, UnityEngine.GUI.skin.textArea, UnityEngine.GUILayout.ExpandHeight(true));
        if (selectedTest != -1)
        {
            UnityEngine.GUIStyle gUIStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);
            gUIStyle.wordWrap = true;
            gUIStyle.richText = true;
            gUIStyle.alignment = UnityEngine.TextAnchor.MiddleCenter;
            UnityEngine.GUIStyle gUIStyle2 = new UnityEngine.GUIStyle();
            UnityEditor.EditorGUILayout.LabelField("<b>" + EditorConfiguration.MyTests[selectedTest].TestName + "</b>", gUIStyle);


            UnityEditor.EditorGUILayout.Separator();
            string textToDisplayForMessage;
            if (EditorConfiguration.MyTests[selectedTest].Status == 0)
            {
                textToDisplayForMessage = "No informartion about this test available.\nPlease rerun the test.";
                UnityEditor.EditorGUILayout.LabelField(textToDisplayForMessage, gUIStyle, UnityEngine.GUILayout.MinWidth(30));
            }
            else
            {
                gUIStyle = new UnityEngine.GUIStyle(UnityEngine.GUI.skin.label);
                gUIStyle.wordWrap = true;
                gUIStyle.richText = true;

                string status = "";
                switch (EditorConfiguration.MyTests[selectedTest].Status)
                {
                    case 1:
                        status = "<color=green>Passed</color>";
                        break;
                    case -1:
                        status = "<color=red>Failed</color>";
                        break;

                }


                UnityEngine.GUILayout.BeginHorizontal();
                UnityEditor.EditorGUILayout.LabelField("<b>Time</b>", gUIStyle, UnityEngine.GUILayout.MinWidth(30));
                UnityEditor.EditorGUILayout.LabelField(EditorConfiguration.MyTests[selectedTest].TestDuration.ToString(), gUIStyle, UnityEngine.GUILayout.MinWidth(100));
                UnityEngine.GUILayout.EndHorizontal();

                UnityEngine.GUILayout.BeginHorizontal();
                UnityEditor.EditorGUILayout.LabelField("<b>Status</b>", gUIStyle, UnityEngine.GUILayout.MinWidth(30));
                UnityEditor.EditorGUILayout.LabelField(status, gUIStyle, UnityEngine.GUILayout.MinWidth(100));
                UnityEngine.GUILayout.EndHorizontal();
                if (EditorConfiguration.MyTests[selectedTest].Status == -1)
                {
                    UnityEngine.GUILayout.BeginHorizontal();
                    UnityEditor.EditorGUILayout.LabelField("<b>Message</b>", gUIStyle, UnityEngine.GUILayout.MinWidth(30));
                    UnityEditor.EditorGUILayout.LabelField(EditorConfiguration.MyTests[selectedTest].TestResultMessage, gUIStyle, UnityEngine.GUILayout.MinWidth(100));
                    UnityEngine.GUILayout.EndHorizontal();

                    UnityEngine.GUILayout.BeginHorizontal();
                    UnityEditor.EditorGUILayout.LabelField("<b>StackTrace</b>", gUIStyle, UnityEngine.GUILayout.MinWidth(30));
                    UnityEditor.EditorGUILayout.LabelField(EditorConfiguration.MyTests[selectedTest].TestStackTrace, gUIStyle, UnityEngine.GUILayout.MinWidth(100));
                    UnityEngine.GUILayout.EndHorizontal();
                }
            }
        }
        else
            UnityEditor.EditorGUILayout.LabelField("No test selected");

        UnityEditor.EditorGUILayout.EndScrollView();
        UnityEditor.EditorGUILayout.EndVertical();
        UnityEditor.EditorGUILayout.EndHorizontal();
    }

    private void AfterExitPlayMode()
    {
        RemoveAltUnityRunnerPrefab();
        AltUnityBuilder.RemoveAltUnityTesterFromScriptingDefineSymbols(UnityEditor.BuildPipeline.GetBuildTargetGroup(UnityEditor.EditorUserBuildSettings.activeBuildTarget));
        EditorConfiguration.ranInEditor = false;
    }

    private static void RemoveAltUnityRunnerPrefab()
    {
        var activeScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
        var altUnityRunners = activeScene.GetRootGameObjects()
            .Where(gameObject => gameObject.name.Equals("AltUnityRunnerPrefab")).ToList();
        if (altUnityRunners.Count != 0)
        {
            foreach (var altUnityRunner in altUnityRunners)
            {
                DestroyImmediate(altUnityRunner);

            }
            //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
        }

    }


    private void RunInEditor()
    {
        AltUnityBuilder.InsertAltUnityInTheActiveScene();
        AltUnityBuilder.CreateJsonFileForInputMappingOfAxis();

        /// Stops marking the scripts as dirty causing the IDE to recompile them
        //AltUnityBuilder.AddAltUnityTesterInScritpingDefineSymbolsGroup(UnityEditor.BuildPipeline.GetBuildTargetGroup(UnityEditor.EditorUserBuildSettings.activeBuildTarget));
        UnityEditor.EditorApplication.isPlaying = true;

    }

    private static void UnindentedLabelAndCheckboxHorizontalLayout(string label, ref bool editorConfigVariable) {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        //UnityEditor.EditorGUILayout.LabelField("", UnityEngine.GUILayout.MaxWidth(30));
        UnityEditor.EditorGUILayout.LabelField(label, UnityEngine.GUILayout.Width(145));
        editorConfigVariable =
            UnityEditor.EditorGUILayout.Toggle(editorConfigVariable, UnityEngine.GUILayout.MaxWidth(30));
        UnityEngine.GUILayout.FlexibleSpace();
        UnityEditor.EditorGUILayout.EndHorizontal();
    }

    public static void SelectAllScenes()
    {
        foreach (var scene in EditorConfiguration.Scenes)
        {
            scene.ToBeBuilt = true;
        }
        UnityEditor.EditorBuildSettings.scenes = PathFromTheSceneInCurrentList();
    }


    private void DisplayTestGui(System.Collections.Generic.List<AltUnityMyTest> tests)
    {
        UnityEditor.EditorGUILayout.LabelField("Tests list", UnityEditor.EditorStyles.boldLabel);
        UnityEditor.EditorGUILayout.BeginHorizontal();
        UnityEditor.EditorGUILayout.EndHorizontal();
        UnityEditor.EditorGUILayout.BeginVertical(UnityEngine.GUI.skin.textArea);

        int foldOutCounter = 0;
        int testCounter = 0;
        var parentNames = new List<string>();
        foreach (var test in tests)
        {
            if(test.TestCaseCount == 0)
            {
                continue;
            }
            if(foldOutCounter > 0 && test.Type == typeof(NUnit.Framework.Internal.TestMethod))
            {
                foldOutCounter--;
                continue;
            }
            if(foldOutCounter > 0)
            {
                continue;
            }
            testCounter++;
            var idx = parentNames.IndexOf(test.ParentName) + 1;
            parentNames.RemoveRange(idx, parentNames.Count - idx);

            if (tests.IndexOf(test) == selectedTest)
            {
                UnityEngine.GUIStyle selectedTestStyle = new UnityEngine.GUIStyle();
                selectedTestStyle.normal.background = selectedTestTexture;
                UnityEditor.EditorGUILayout.BeginHorizontal(selectedTestStyle);
            }
            else
            {
                if (testCounter % 2 == 0)
                {
                    UnityEngine.GUIStyle evenNumberStyle = new UnityEngine.GUIStyle();
                    evenNumberStyle.normal.background = evenNumberTestTexture;
                    UnityEditor.EditorGUILayout.BeginHorizontal(evenNumberStyle);
                }
                else
                {
                    UnityEngine.GUIStyle oddNumberStyle = new UnityEngine.GUIStyle();
                    oddNumberStyle.normal.background = oddNumberTestTexture;
                    UnityEditor.EditorGUILayout.BeginHorizontal(oddNumberStyle);
                }
            }
            UnityEditor.EditorGUILayout.LabelField(" ", UnityEngine.GUILayout.Width(30 * parentNames.Count));
            UnityEngine.GUIStyle gUIStyle = new UnityEngine.GUIStyle();
            gUIStyle.alignment = UnityEngine.TextAnchor.MiddleLeft;
            var valueChanged = UnityEditor.EditorGUILayout.Toggle(test.Selected, UnityEngine.GUILayout.Width(15));
            if (valueChanged != test.Selected)
            {
                test.Selected = valueChanged;
                ChangeSelectionChildsAndParent(test);
            }

            var testName = test.TestName;

            if (test.ParentName == "")
            {
                var splitedPath = testName.Split('/');
                testName = splitedPath[splitedPath.Length - 1];
            }
            else
            {
                var splitedPath = testName.Split('.');
                testName = splitedPath[splitedPath.Length - 1];
            }


            if (test.Status == 0)
            {
                UnityEngine.GUIStyle guiStyle = new UnityEngine.GUIStyle { normal = { textColor = UnityEditor.EditorGUIUtility.isProSkin ? UnityEngine.Color.white : UnityEngine.Color.black } };
                SelectTest(tests, test, testName, guiStyle);
            }
            else
            {
                UnityEngine.Color color = redColor;
                UnityEngine.Texture2D icon = failIcon;
                if (test.Status == 1)
                {
                    color = greenColor;
                    icon = passIcon;
                }
                UnityEngine.GUILayout.Label(icon, gUIStyle, UnityEngine.GUILayout.Width(20));
                UnityEngine.GUIStyle guiStyle = new UnityEngine.GUIStyle { normal = { textColor = color } };
                SelectTest(tests, test, testName, guiStyle);
            }
            UnityEngine.GUILayout.FlexibleSpace();
            if (test.Type == typeof(NUnit.Framework.Internal.TestMethod))
            {
                test.FoldOut = true;
            }
            else
            {
                test.FoldOut = UnityEditor.EditorGUILayout.Foldout(test.FoldOut, "");
                if (!test.FoldOut)
                {
                    foldOutCounter = test.TestCaseCount;
                }
                parentNames.Add(test.TestName);
            }
            UnityEditor.EditorGUILayout.EndHorizontal();

        }
        UnityEditor.EditorGUILayout.EndVertical();
    }

    private static void SelectTest(System.Collections.Generic.List<AltUnityMyTest> tests, AltUnityMyTest test, string testName, UnityEngine.GUIStyle guiStyle)
    {
        if (!test.IsSuite)
        {
            if (UnityEngine.GUILayout.Button(testName, guiStyle))
            {
                if (selectedTest == tests.IndexOf(test))
                {
                    var actualTime = System.DateTime.Now.Ticks;
                    if (actualTime - timeSinceLastClick < 5000000)
                    {
#if UNITY_2019_1_OR_NEWER
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(test.path, 1,0);
#else
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(test.path, 1);
#endif
                    }
                }
                else
                {
                    selectedTest = tests.IndexOf(test);

                }
                timeSinceLastClick = System.DateTime.Now.Ticks;
            }
        }
        else
        {
            UnityEngine.GUILayout.Label(testName, guiStyle);
        }
    }

    private void ChangeSelectionChildsAndParent(AltUnityMyTest test)
    {
        if (test.Type.ToString().Equals("NUnit.Framework.Internal.TestAssembly"))
        {
            var index = EditorConfiguration.MyTests.IndexOf(test);
            for(int i= index + 1; i < EditorConfiguration.MyTests.Count; i++)
            {
                if (EditorConfiguration.MyTests[i].Type.ToString().Equals("NUnit.Framework.Internal.TestAssembly")){
                    break;
                }
                else
                {
                    EditorConfiguration.MyTests[i].Selected = test.Selected;
                }
            }
        }
        else
        {
                if (test.IsSuite)
                {
                    var index = EditorConfiguration.MyTests.IndexOf(test);
                    for (int i = index + 1; i <= index + test.TestCaseCount; i++)
                    {
                        EditorConfiguration.MyTests[i].Selected = test.Selected;
                    }
                }
            if (test.Selected == false)
            {
                while (test.ParentName != null)
                {
                    test = EditorConfiguration.MyTests.FirstOrDefault(a => a.TestName.Equals(test.ParentName));
                    if (test != null)
                        test.Selected = false;
                    else
                        return;
                }
            }
                
        }

    }

    public static void AddAllScenes()
    {
        var scenesToBeAddedGuid = UnityEditor.AssetDatabase.FindAssets("t:SceneAsset");
        EditorConfiguration.Scenes = new System.Collections.Generic.List<AltUnityMyScenes>();
        foreach (var sceneGuid in scenesToBeAddedGuid)
        {
            var scenePath = UnityEditor.AssetDatabase.GUIDToAssetPath(sceneGuid);
            EditorConfiguration.Scenes.Add(new AltUnityMyScenes(false, scenePath, 0));

        }

        UnityEditor.EditorBuildSettings.scenes = PathFromTheSceneInCurrentList();

    }

    private static UnityEditor.EditorBuildSettingsScene[] PathFromTheSceneInCurrentList()
    {
        System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene> listofPath = new System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene>();
        foreach (var scene in EditorConfiguration.Scenes)
        {
            listofPath.Add(new UnityEditor.EditorBuildSettingsScene(scene.Path, scene.ToBeBuilt));
        }

        return listofPath.ToArray();
    }

    private UnityEngine.Texture2D MakeTexture(int width, int height, UnityEngine.Color col)
    {
        UnityEngine.Color[] pix = new UnityEngine.Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        UnityEngine.Texture2D result = new UnityEngine.Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }



    [UnityEditor.MenuItem("Assets/Create/AltUnityTest", false, 80)]
    public static void CreateAltUnityTest()
    {

        var templatePath = UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("DefaultTestExample")[0]);
        string folderPath = GetPathForSelectedItem();
        string newFilePath = System.IO.Path.Combine(folderPath, "NewAltUnityTest.cs");
#if UNITY_2019_1_OR_NEWER
        UnityEditor.ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, newFilePath);
#else
        System.Reflection.MethodInfo method = typeof(UnityEditor.ProjectWindowUtil).GetMethod("CreateScriptAsset", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        if (method == null)
            throw new Assets.AltUnityTester.AltUnityDriver.NotFoundException("Method to create Script file was not found");
        method.Invoke((object)null, new object[2]
        {
            (object) templatePath,
            (object) newFilePath
        });
#endif

    }

    [UnityEditor.MenuItem("Assets/Create/AltUnityTest", true, 80)]
    public static bool CanCreateAltUnityTest()
    {
        return (GetPathForSelectedItem() + "/").Contains("/Editor/");
    }

    [UnityEditor.MenuItem("Window/Just Unity Tester/Create Tester Package")]
    public static void CreateAltUnityTesterPackage()
    {
        UnityEngine.Debug.Log("AltUnityTester - Unity Package creation started...");
        string packageName = "AltUnityTester.unitypackage";
        string assetPathNames = "Assets/AltUnityTester";
        UnityEditor.AssetDatabase.ExportPackage(assetPathNames, packageName, UnityEditor.ExportPackageOptions.Recurse);
        UnityEngine.Debug.Log("AltUnityTester - Unity Package done.");
    }

    private static string GetPathForSelectedItem()
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);
        if (System.IO.Path.GetExtension(path) != "") //checks if current item is a folder or a file
        {
            path = path.Replace(System.IO.Path.GetFileName(UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject)), "");
        }
        return path;
    }

    private static void DestroyAltUnityRunner(UnityEngine.Object altUnityRunner)
    {

        DestroyImmediate(altUnityRunner);
        //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
        UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(AltUnityBuilder.PreviousScenePath);
    }

}
