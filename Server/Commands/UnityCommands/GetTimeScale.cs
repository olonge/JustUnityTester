namespace JustUnityTester.Server.Commands {
    class GetTimeScale : Command {
        public override string Execute() {
            TestRunner.Instance.LogMessage("GetTimeScale");
            string response = TestRunner.Instance.errorCouldNotPerformOperationMessage;
            response = Newtonsoft.Json.JsonConvert.SerializeObject(UnityEngine.Time.timeScale);
            return response;
        }
    }
}
