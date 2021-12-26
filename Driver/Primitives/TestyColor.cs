namespace JustUnityTester.Driver.Primitives {
    public struct TestyColor {
        public float r;
        public float g;
        public float b;
        public float a;
        public TestyColor(float r, float g, float b) {
            this.r = r;
            this.g = g;
            this.b = b;
            a = 1;

        }
        public TestyColor(float r, float g, float b, float a) {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;

        }
    }
}
