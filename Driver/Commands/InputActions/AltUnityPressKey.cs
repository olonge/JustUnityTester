using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityPressKey : BaseCommand {
        AltUnityKeyCode keyCode;
        float power;
        float duration;
        public AltUnityPressKey(SocketSettings socketSettings, AltUnityKeyCode keyCode, float power, float duration) : base(socketSettings) {
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