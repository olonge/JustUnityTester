namespace JustUnityTester.Server.Commands {
    class AltUnityEnableLoggingCommand : AltUnityCommand {
        bool activateDebug;

        public AltUnityEnableLoggingCommand(bool activateDebug) {
            this.activateDebug = activateDebug;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.logEnabled = activateDebug;
            TestRunner._altUnityRunner.LogMessage("Logging is set to " + activateDebug);
            return "Ok";
        }
    }
}
