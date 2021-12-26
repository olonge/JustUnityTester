namespace JustUnityTester.Server.Commands {
    class AltUnityUnknowStringCommand : AltUnityCommand {
        public override string Execute() {
            return AltUnityRunner._altUnityRunner.errorUnknownError;
        }
    }
}
