using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Core {
    public struct TestTextureInformation {
        public byte[] imageData;
        public AltUnityVector2 scaleDifference;
        public AltUnityVector3 textureSize;
        public AltUnityTextureFormat textureFormat;

        public TestTextureInformation(byte[] imageData, AltUnityVector2 scaleDifference, AltUnityVector3 textureSize, AltUnityTextureFormat textureFormat) {
            this.imageData = imageData;
            this.scaleDifference = scaleDifference;
            this.textureSize = textureSize;
            this.textureFormat = textureFormat;
        }
    }
}