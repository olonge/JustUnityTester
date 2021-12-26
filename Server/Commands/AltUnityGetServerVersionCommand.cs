namespace JustUnityTester.Server.Commands {
    class AltUnityGetServerVersionCommand : AltUnityCommand {
        public override string Execute() {
            TestRunner.Instance.LogMessage("Server version is: " + TestRunner.VERSION);
            return TestRunner.VERSION;
        }
    }
}
