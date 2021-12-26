namespace JustUnityTester.Server.Commands {
    class GetServerVersion : Command {
        public override string Execute() {
            TestRunner.Instance.LogMessage("Server version is: " + TestRunner.VERSION);
            return TestRunner.VERSION;
        }
    }
}
