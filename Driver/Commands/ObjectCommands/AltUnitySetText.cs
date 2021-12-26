using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnitySetText : AltUnityCommandReturningAltElement {
        AltUnityObject altUnityObject;
        string newText;

        public AltUnitySetText(SocketSettings socketSettings, AltUnityObject altUnityObject, string text) : base(socketSettings) {
            this.altUnityObject = altUnityObject;
            newText = text;
        }

        public AltUnityObject Execute() {
            var altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("setText", altObject, newText)));
            return ReceiveAltUnityObject();
        }
    }
}