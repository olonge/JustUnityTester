using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityGetCurrentScene : AltBaseCommand {
        public AltUnityGetCurrentScene(SocketSettings socketSettings) : base(socketSettings) {
        }
        public string Execute() {
            Socket.Client.Send(toBytes(CreateCommand("getCurrentScene")));
            string data = Recvall();
            if (!data.Contains("error:")) return Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(data).name;
            HandleErrors(data);
            return null;
        }
    }
}