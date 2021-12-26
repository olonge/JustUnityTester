using NUnit.Framework.Interfaces;

namespace JustUnityTester.Editor {
    public class TestRunListener : ITestListener {
        public readonly TestRunDelegate CallRunDelegate;
        public TestRunListener(TestRunDelegate callRunDelegate) => CallRunDelegate = callRunDelegate;

        public void TestStarted(ITest test) {
            if (!test.IsSuite)
                CallRunDelegate?.Invoke(test.Name);
        }

        public void TestFinished(ITestResult result) {
            if (!result.Test.IsSuite) {
                UnityEngine.Debug.Log("==============> TEST " + result.Test.FullName + ": " + result.ResultState.ToString().ToUpper());

                if (result.ResultState != ResultState.Success) {
                    UnityEngine.Debug.Log("Error Message: " + result.Message);
                    UnityEngine.Debug.Log(result.StackTrace);
                }

                UnityEngine.Debug.Log("======================================================");
            }
        }

        public void TestOutput(TestOutput output) { }
    }
}