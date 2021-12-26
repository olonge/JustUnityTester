namespace JustUnityTester.Server.Commands {
    class FindAllObjects : AltUnityCommand {
        string methodParameter;

        public FindAllObjects(string methodParameter) {
            this.methodParameter = methodParameter;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("all objects requested");
            var parameters = ";" + methodParameter;
            return new AltUnityFindObjectsByNameCommand(parameters).Execute();
        }
    }
}
