using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityPointerExitObject : ReturnedElement {
        TestObject altUnityObject;

        public AltUnityPointerExitObject(SocketSettings socketSettings, TestObject altUnityObject) : base(socketSettings) {
            this.altUnityObject = altUnityObject;
        }
        public TestObject Execute() {
            string altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("pointerExitObject", altObject)));
            return ReceiveAltUnityObject();
        }
    }
}