namespace JustUnityTester.Server.Commands {
    class AltUnityUnknowStringCommand : AltUnityCommand {
        public override string Execute() {
            return TestRunner._altUnityRunner.errorUnknownError;
        }
    }
}
