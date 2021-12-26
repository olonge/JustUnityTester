namespace JustUnityTester.Server.Commands {
    class AltUnityGetTimeScaleCommand : AltUnityCommand {
        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("GetTimeScale");
            string response = TestRunner._altUnityRunner.errorCouldNotPerformOperationMessage;
            response = Newtonsoft.Json.JsonConvert.SerializeObject(UnityEngine.Time.timeScale);
            return response;
        }
    }
}
