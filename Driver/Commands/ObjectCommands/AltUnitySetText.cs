using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnitySetText : ReturnedElement {
        TestObject altUnityObject;
        string newText;

        public AltUnitySetText(SocketSettings socketSettings, TestObject altUnityObject, string text) : base(socketSettings) {
            this.altUnityObject = altUnityObject;
            newText = text;
        }

        public TestObject Execute() {
            var altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("setText", altObject, newText)));
            return ReceiveAltUnityObject();
        }
    }
}