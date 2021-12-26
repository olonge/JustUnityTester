namespace JustUnityTester.Server.Commands {
    class SetTimeScale : AltUnityCommand {
        float timeScale;

        public SetTimeScale(float timeScale) {
            this.timeScale = timeScale;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("SetTimeScale to: " + timeScale);
            string response = TestRunner.Instance.errorCouldNotPerformOperationMessage;
            UnityEngine.Time.timeScale = timeScale;
            response = "Ok";
            return response;
        }
    }
}
