using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityTapScreen : BaseCommand {
        float x;
        float y;
        public AltUnityTapScreen(SocketSettings socketSettings, float x, float y) : base(socketSettings) {
            this.x = x;
            this.y = y;
        }
        public TestObject Execute() {
            Socket.Client.Send(toBytes(CreateCommand("tapScreen", x.ToString(), y.ToString())));
            string data = Recvall();
            if (!data.Contains("error:")) return Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(data);
            if (data.Contains("error:notFound")) return null;
            HandleErrors(data);
            return null;
        }
    }
}