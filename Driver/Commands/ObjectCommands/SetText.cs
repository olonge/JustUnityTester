using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class SetText : ReturnedElement {
        TestObject altUnityObject;
        string newText;

        public SetText(SocketSettings socketSettings, TestObject altUnityObject, string text) : base(socketSettings) {
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