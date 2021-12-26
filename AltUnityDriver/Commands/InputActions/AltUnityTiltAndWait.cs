using Assets.AltUnityTester.AltUnityDriver.UnityStruct;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityTiltAndWait : AltBaseCommand {
        AltUnityVector3 acceleration;
        float duration;
        public AltUnityTiltAndWait(SocketSettings socketSettings, AltUnityVector3 acceleration, float duration) : base(socketSettings) {
            this.acceleration = acceleration;
            this.duration = duration;
        }
        public void Execute() {
            new AltUnityTilt(SocketSettings, acceleration, duration).Execute();
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