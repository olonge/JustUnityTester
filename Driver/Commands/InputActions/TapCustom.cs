namespace JustUnityTester.Driver.Commands {
    public class TapCustom : BaseCommand {
        float x;
        float y;
        int count;
        float interval;
        public TapCustom(SocketSettings socketSettings, float x, float y, int count, float interval) : base(socketSettings) {
            this.x = x;
            this.y = y;
            this.count = count;
            this.interval = interval;
        }
        public void Execute() {
            var posJson = PositionToJson(x, y);
            Socket.Client.Send(toBytes(CreateCommand("tapCustom", posJson, count.ToString(), interval.ToString())));
            string data = Recvall();
            if (data.Equals("OK")) return;
            HandleErrors(data);
        }
    }
}