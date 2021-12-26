namespace JustUnityTester.Server.Commands {
    class AltUnityGetKeyPlayerPrefCommand : AltUnityCommand {
        PLayerPrefKeyType type;
        string value;

        public AltUnityGetKeyPlayerPrefCommand(PLayerPrefKeyType type, string value) {
            this.type = type;
            this.value = value;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("getKeyPlayerPref for: " + value);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            if (UnityEngine.PlayerPrefs.HasKey(value)) {
                switch (type) {
                    case PLayerPrefKeyType.String:
                        TestRunner._altUnityRunner.LogMessage("Option string " + UnityEngine.PlayerPrefs.GetString(value));
                        response = UnityEngine.PlayerPrefs.GetString(value);
                        break;
                    case PLayerPrefKeyType.Float:
                        TestRunner._altUnityRunner.LogMessage("Option Float " + UnityEngine.PlayerPrefs.GetFloat(value));
                        response = UnityEngine.PlayerPrefs.GetFloat(value) + "";
                        break;
                    case PLayerPrefKeyType.Int:
                        TestRunner._altUnityRunner.LogMessage("Option Int " + UnityEngine.PlayerPrefs.GetInt(value));
                        response = UnityEngine.PlayerPrefs.GetInt(value) + "";
                        break;
                }
            }
            return response;
        }
    }
}
