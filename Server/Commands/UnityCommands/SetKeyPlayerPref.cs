namespace JustUnityTester.Server.Commands {
    class SetKeyPlayerPref : AltUnityCommand {
        PLayerPrefKeyType type;
        string keyName;
        string value;

        public SetKeyPlayerPref(PLayerPrefKeyType type, string keyName, string value) {
            this.type = type;
            this.keyName = keyName;
            this.value = value;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("setKeyPlayerPref for: " + keyName);
            string response = TestRunner.Instance.errorNotFoundMessage;
            switch (type) {
                case PLayerPrefKeyType.String:
                    TestRunner.Instance.LogMessage("Set Option string ");
                    UnityEngine.PlayerPrefs.SetString(keyName, value);
                    break;
                case PLayerPrefKeyType.Float:
                    TestRunner.Instance.LogMessage("Set Option Float ");
                    UnityEngine.PlayerPrefs.SetFloat(keyName, float.Parse(value));
                    break;
                case PLayerPrefKeyType.Int:
                    TestRunner.Instance.LogMessage("Set Option Int ");
                    UnityEngine.PlayerPrefs.SetInt(keyName, int.Parse(value));
                    break;
            }
            response = "Ok";
            return response;
        }
    }
}
