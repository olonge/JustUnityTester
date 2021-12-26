namespace JustUnityTester.Server.Commands {
    class AltUnityGetServerVersionCommand : AltUnityCommand {
        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("Server version is: " + TestRunner.VERSION);
            return TestRunner.VERSION;
        }
    }
}
