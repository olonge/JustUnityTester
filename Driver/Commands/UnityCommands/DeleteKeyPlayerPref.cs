namespace JustUnityTester.Driver.Commands {
    public class DeleteKeyPlayerPref : BaseCommand {
        string keyName;
        public DeleteKeyPlayerPref(SocketSettings socketSettings, string keyname) : base(socketSettings) {
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