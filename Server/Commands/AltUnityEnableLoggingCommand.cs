namespace JustUnityTester.Server.Commands {
    class AltUnityEnableLoggingCommand : AltUnityCommand {
        bool activateDebug;

        public AltUnityEnableLoggingCommand(bool activateDebug) {
            this.activateDebug = activateDebug;
        }

        public override string Execute() {
            TestRunner.Instance.logEnabled = activateDebug;
            TestRunner.Instance.LogMessage("Logging is set to " + activateDebug);
            return "Ok";
        }
    }
}
