namespace JustUnityTester.Server.Commands {
    class GetKeyPlayerPref : AltUnityCommand {
        PLayerPrefKeyType type;
        string value;

        public GetKeyPlayerPref(PLayerPrefKeyType type, string value) {
            this.type = type;
            this.value = value;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("getKeyPlayerPref for: " + value);
            string response = TestRunner.Instance.errorNotFoundMessage;
            if (UnityEngine.PlayerPrefs.HasKey(value)) {
                switch (type) {
                    case PLayerPrefKeyType.String:
                        TestRunner.Instance.LogMessage("Option string " + UnityEngine.PlayerPrefs.GetString(value));
                        response = UnityEngine.PlayerPrefs.GetString(value);
                        break;
                    case PLayerPrefKeyType.Float:
                        TestRunner.Instance.LogMessage("Option Float " + UnityEngine.PlayerPrefs.GetFloat(value));
                        response = UnityEngine.PlayerPrefs.GetFloat(value) + "";
                        break;
                    case PLayerPrefKeyType.Int:
                        TestRunner.Instance.LogMessage("Option Int " + UnityEngine.PlayerPrefs.GetInt(value));
                        response = UnityEngine.PlayerPrefs.GetInt(value) + "";
                        break;
                }
            }
            return response;
        }
    }
}
