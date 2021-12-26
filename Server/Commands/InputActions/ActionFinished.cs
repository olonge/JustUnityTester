namespace JustUnityTester.Server.Commands {
    class ActionFinished : Command {
        public override string Execute() {
            string response = TestRunner.Instance.errorNotFoundMessage;
#if ALTUNITYTESTER
                AltUnityRunner._altUnityRunner.LogMessage("actionFinished");
                if (Input.Finished)
                    response = "Yes";
                else
                {
                    response = "No";
                }
#endif
            return response;
        }
    }
}
