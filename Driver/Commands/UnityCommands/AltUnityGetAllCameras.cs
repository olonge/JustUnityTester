using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityGetAllCameras : BaseFindObjects {
        public AltUnityGetAllCameras(SocketSettings socketSettings) : base(socketSettings) {
        }
        public System.Collections.Generic.List<TestObject> Execute() {
            Socket.Client.Send(toBytes(CreateCommand("getAllCameras")));
            return ReceiveListOfAltUnityObjects();

        }
    }
}