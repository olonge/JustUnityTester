using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class MoveMouse : BaseCommand {
        TestVector2 location;
        float duration;
        public MoveMouse(SocketSettings socketSettings, TestVector2 location, float duration) : base(socketSettings) {
            this.location = location;
            this.duration = duration;
        }
        public void Execute() {
            var locationJson = PositionToJson(location);
            Socket.Client.Send(toBytes(CreateCommand("moveMouse", locationJson, duration.ToString())));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);
        }
    }
}