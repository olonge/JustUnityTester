namespace JustUnityTester.Driver.Commands {
    public class SetTimeScale : BaseCommand {
        float timeScale;

        public SetTimeScale(SocketSettings socketSettings, float timescale) : base(socketSettings) {
            timeScale = timescale;
        }
        public void Execute() {
            Socket.Client.Send(toBytes(CreateCommand("setTimeScale", Newtonsoft.Json.JsonConvert.SerializeObject(timeScale))));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);

        }
    }
}