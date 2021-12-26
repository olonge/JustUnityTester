using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class FindObjectsWhichContain : BaseFindObjects {
        By by;
        string value;
        By cameraBy;
        string cameraPath;
        bool enabled;

        public FindObjectsWhichContain(SocketSettings socketSettings, By by, string value, By cameraBy, string cameraPath, bool enabled) : base(socketSettings) {
            this.by = by;
            this.value = value;
            this.cameraBy = cameraBy;
            this.cameraPath = cameraPath;
            this.enabled = enabled;
        }
        public System.Collections.Generic.List<TestObject> Execute() {
            string path = SetPathContains(by, value);
            cameraPath = SetPath(cameraBy, cameraPath);
            Socket.Client.Send(toBytes(CreateCommand("findObjects", path, cameraBy.ToString(), cameraPath, enabled.ToString())));
            return ReceiveListOfAltUnityObjects();
        }
    }
}