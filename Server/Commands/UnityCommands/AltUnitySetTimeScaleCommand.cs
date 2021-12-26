namespace JustUnityTester.Server.Commands {
    class AltUnitySetTimeScaleCommand : AltUnityCommand {
        float timeScale;

        public AltUnitySetTimeScaleCommand(float timeScale) {
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
