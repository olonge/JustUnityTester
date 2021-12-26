namespace JustUnityTester.Server.Commands {
    class AltUnityFindAllObjectsCommand : AltUnityCommand {
        string methodParameter;

        public AltUnityFindAllObjectsCommand(string methodParameter) {
            this.methodParameter = methodParameter;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("all objects requested");
            var parameters = ";" + methodParameter;
            return new AltUnityFindObjectsByNameCommand(parameters).Execute();
        }
    }
}
