using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityGetText : BaseCommand {
        TestObject altUnityObject;

        public AltUnityGetText(SocketSettings socketSettings, TestObject altUnityObject) : base(socketSettings) {
            this.altUnityObject = altUnityObject;
        }

        public string Execute() {
            string altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("getText", altObject)));
            string data = Recvall();
            if (!data.Contains("error:")) return data;
            HandleErrors(data);
            return null;
        }
    }
}