using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class TiltAndWait : BaseCommand {
        TestVector3 acceleration;
        float duration;
        public TiltAndWait(SocketSettings socketSettings, TestVector3 acceleration, float duration) : base(socketSettings) {
            this.acceleration = acceleration;
            this.duration = duration;
        }
        public void Execute() {
            new Tilt(SocketSettings, acceleration, duration).Execute();
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