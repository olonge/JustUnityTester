namespace JustUnityTester.Server.Commands {
    class AltUnitySetTimeScaleCommand : AltUnityCommand {
        float timeScale;

        public AltUnitySetTimeScaleCommand(float timeScale) {
            this.timeScale = timeScale;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("SetTimeScale to: " + timeScale);
            string response = TestRunner._altUnityRunner.errorCouldNotPerformOperationMessage;
            UnityEngine.Time.timeScale = timeScale;
            response = "Ok";
            return response;
        }
    }
}
