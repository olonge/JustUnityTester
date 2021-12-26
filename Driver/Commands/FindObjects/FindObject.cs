using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class FindObject : BaseFindObjects {
        By by;
        string value;
        By cameraBy;
        string cameraPath;
        bool enabled;

        public FindObject(SocketSettings socketSettings, By by, string value, By cameraBy, string cameraPath, bool enabled) : base(socketSettings) {
            this.by = by;
            this.value = value;
            this.cameraBy = cameraBy;
            this.cameraPath = cameraPath;
            this.enabled = enabled;
        }
        public TestObject Execute() {
            cameraPath = SetPath(cameraBy, cameraPath);
            if (enabled && by == By.NAME) {
                Socket.Client.Send(toBytes(CreateCommand("findActiveObjectByName", value, cameraBy.ToString(), cameraPath, enabled.ToString())));
            } else {
                string path = SetPath(by, value);
                Socket.Client.Send(toBytes(CreateCommand("findObject", path, cameraBy.ToString(), cameraPath, enabled.ToString())));
            }
            return ReceiveAltUnityObject();
        }
    }
}