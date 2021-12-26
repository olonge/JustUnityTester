using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class PressKey : BaseCommand {
        TestKeyCode keyCode;
        float power;
        float duration;
        public PressKey(SocketSettings socketSettings, TestKeyCode keyCode, float power, float duration) : base(socketSettings) {
            this.keyCode = keyCode;
            this.power = power;
            this.duration = duration;
        }
        public void Execute() {
            Socket.Client.Send(toBytes(CreateCommand("pressKeyboardKey", keyCode.ToString(), power.ToString(), duration.ToString())));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);
        }
    }
}