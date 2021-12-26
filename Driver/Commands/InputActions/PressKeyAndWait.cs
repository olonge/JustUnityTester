using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class PressKeyAndWait : BaseCommand {
        AltUnityKeyCode keyCode;
        float power;
        float duration;
        public PressKeyAndWait(SocketSettings socketSettings, AltUnityKeyCode keyCode, float power, float duration) : base(socketSettings) {
            this.keyCode = keyCode;
            this.power = power;
            this.duration = duration;
        }
        public void Execute() {
            new PressKey(SocketSettings, keyCode, power, duration).Execute();
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