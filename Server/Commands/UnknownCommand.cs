namespace JustUnityTester.Server.Commands {
    class UnknownCommand : Command {
        public override string Execute() {
            return TestRunner.Instance.errorUnknownError;
        }
    }
}
