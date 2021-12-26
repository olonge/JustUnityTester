namespace JustUnityTester.Server.Commands {
    class AltUnityDeleteKeyPlayerPrefCommand : AltUnityCommand {
        string keyName;

        public AltUnityDeleteKeyPlayerPrefCommand(string keyName) {
            this.keyName = keyName;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("deleteKeyPlayerPref for: " + keyName);
            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.PlayerPrefs.DeleteKey(keyName);
            response = "Ok";
            return response;
        }
    }
}
