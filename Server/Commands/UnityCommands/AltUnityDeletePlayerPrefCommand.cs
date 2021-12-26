namespace JustUnityTester.Server.Commands {
    class AltUnityDeletePlayerPrefCommand : AltUnityCommand {

        public override string Execute() {
            TestRunner.Instance.LogMessage("deletePlayerPref");
            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.PlayerPrefs.DeleteAll();
            response = "Ok";
            return response;
        }
    }
}
