namespace JustUnityTester.Core {
    [System.Serializable]
    public class Axis {
        public string name;
        public string negativeButton;
        public string positiveButton;
        public string altPositiveButton;
        public string altNegativeButton;

        public Axis(string name, string negativeButton, string positiveButton, string altPositiveButton, string altNegativeButton) {
            this.name = name;
            this.negativeButton = negativeButton;
            this.positiveButton = positiveButton;
            this.altPositiveButton = altPositiveButton;
            this.altNegativeButton = altNegativeButton;
        }


    }
}