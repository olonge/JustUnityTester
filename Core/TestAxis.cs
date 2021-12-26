namespace JustUnityTester.Core {
    [System.Serializable]
    public class TestAxis {
        public string name;
        public string negativeButton;
        public string positiveButton;
        public string altPositiveButton;
        public string altNegativeButton;

        public TestAxis(string name, string negativeButton, string positiveButton, string altPositiveButton, string altNegativeButton) {
            this.name = name;
            this.negativeButton = negativeButton;
            this.positiveButton = positiveButton;
            this.altPositiveButton = altPositiveButton;
            this.altNegativeButton = altNegativeButton;
        }


    }
}