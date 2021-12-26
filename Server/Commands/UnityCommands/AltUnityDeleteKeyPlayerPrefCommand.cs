namespace JustUnityTester.Server.Commands {
    class AltUnityDeleteKeyPlayerPrefCommand : AltUnityCommand {
        string keyName;

        public AltUnityDeleteKeyPlayerPrefCommand(string keyName) {
            this.keyName = keyName;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("deleteKeyPlayerPref for: " + keyName);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.PlayerPrefs.DeleteKey(keyName);
            response = "Ok";
            return response;
        }
    }
}
