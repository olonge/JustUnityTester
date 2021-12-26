using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class MoveMouseAndWait : BaseCommand {
        TestVector2 location;
        float duration;
        public MoveMouseAndWait(SocketSettings socketSettings, TestVector2 location, float duration) : base(socketSettings) {
            this.location = location;
            this.duration = duration;
        }
        public void Execute() {
            new MoveMouse(SocketSettings, location, duration).Execute();
            System.Threading.Thread.Sleep((int)duration * 1000);
            string data;
            do {
                Socket.Client.Send(toBytes(CreateCommand("actionFinished")));
                data = Recvall();
            } while (data == "No");
            if (data.Equals("Yes"))
                return;
            HandleErrors(data);
        }
    }
}