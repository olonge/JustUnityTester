namespace JustUnityTester.Driver.Commands {
    public class AltUnityDeleteKeyPlayerPref : BaseCommand {
        string keyName;
        public AltUnityDeleteKeyPlayerPref(SocketSettings socketSettings, string keyname) : base(socketSettings) {
            keyName = keyname;
        }
        public void Execute() {
            Socket.Client.Send(toBytes(CreateCommand("deleteKeyPlayerPref", keyName)));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);
        }

    }
}