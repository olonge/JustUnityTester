namespace JustUnityTester.Driver.Primitives {
    public struct TestVector2 {
        public float x;
        public float y;

        public TestVector2(float x, float y) {
            this.x = x;
            this.y = y;
        }
    }

    public struct TestVector3 {
        public float x;
        public float y;
        public float z;

        public TestVector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public TestVector3(float x, float y) : this(x, y, 0) {
        }
    }
}
