using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Core {
    public struct TestTextureInformation {
        public byte[] imageData;
        public TestVector2 scaleDifference;
        public TestVector3 textureSize;
        public TestTextureFormat textureFormat;

        public TestTextureInformation(byte[] imageData, TestVector2 scaleDifference, TestVector3 textureSize, TestTextureFormat textureFormat) {
            this.imageData = imageData;
            this.scaleDifference = scaleDifference;
            this.textureSize = textureSize;
            this.textureFormat = textureFormat;
        }
    }
}