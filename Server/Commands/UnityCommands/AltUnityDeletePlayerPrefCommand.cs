namespace JustUnityTester.Server.Commands {
    class AltUnityDeletePlayerPrefCommand : AltUnityCommand {

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("deletePlayerPref");
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.PlayerPrefs.DeleteAll();
            response = "Ok";
            return response;
        }
    }
}
