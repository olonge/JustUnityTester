namespace JustUnityTester.Driver.Commands {
    internal class AltUnityGetAllLoadedScenes : BaseCommand {
        public AltUnityGetAllLoadedScenes(SocketSettings socketSettings) : base(socketSettings) {
        }
        public System.Collections.Generic.List<string> Execute() {
            Socket.Client.Send(toBytes(CreateCommand("getAllLoadedScenes")));
            var response = Recvall();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<string>>(response);

        }
    }
}