namespace JustUnityTester.Server.Commands {
    class EnableLogging : Command {
        bool activateDebug;

        public EnableLogging(bool activateDebug) {
            this.activateDebug = activateDebug;
        }

        public override string Execute() {
            TestRunner.Instance.logEnabled = activateDebug;
            TestRunner.Instance.LogMessage("Logging is set to " + activateDebug);
            return "Ok";
        }
    }
}
