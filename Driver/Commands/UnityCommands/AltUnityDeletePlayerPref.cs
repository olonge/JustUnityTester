namespace JustUnityTester.Driver.Commands {
    public class AltUnityDeletePlayerPref : BaseCommand {
        public AltUnityDeletePlayerPref(SocketSettings socketSettings) : base(socketSettings) {
        }
        public void Execute() {
            Socket.Client.Send(toBytes(CreateCommand("deletePlayerPref")));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);
        }
    }
}