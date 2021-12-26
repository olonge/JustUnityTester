namespace JustUnityTester.Server.Commands {
    class AltUnityActionFinishedCommand : AltUnityCommand {
        public override string Execute() {
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
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
