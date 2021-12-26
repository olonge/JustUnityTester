using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class FindObjectWhichContains : BaseFindObjects {
        By by;
        string value;
        By cameraBy;
        string cameraPath;
        bool enabled;

        public FindObjectWhichContains(SocketSettings socketSettings, By by, string value, By cameraBy, string cameraPath, bool enabled) : base(socketSettings) {
            this.by = by;
            this.value = value;
            this.cameraBy = cameraBy;
            this.cameraPath = cameraPath;
            this.enabled = enabled;
        }
        public TestObject Execute() {
            string path = SetPathContains(by, value);
            cameraPath = SetPath(cameraBy, cameraPath);
            Socket.Client.Send(toBytes(CreateCommand("findObject", path, cameraBy.ToString(), cameraPath, enabled.ToString())));
            return ReceiveAltUnityObject();
        }
    }
}