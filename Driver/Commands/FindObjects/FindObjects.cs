using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class FindObjects : BaseFindObjects {
        By by;
        string value;
        By cameraBy;
        string cameraPath;
        bool enabled;

        public FindObjects(SocketSettings socketSettings, By by, string value, By cameraBy, string cameraPath, bool enabled) : base(socketSettings) {
            this.by = by;
            this.value = value;
            this.cameraBy = cameraBy;
            this.cameraPath = cameraPath;
            this.enabled = enabled;
        }
        public System.Collections.Generic.List<TestObject> Execute() {
            string path = SetPath(by, value);
            cameraPath = SetPath(cameraBy, cameraPath);
            Socket.Client.Send(toBytes(CreateCommand("findObjects", path, cameraBy.ToString(), cameraPath, enabled.ToString())));
            return ReceiveListOfAltUnityObjects();
        }
    }
}