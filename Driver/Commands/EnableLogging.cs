namespace JustUnityTester.Driver.Commands {
    public class EnableLogging : BaseCommand {
        public EnableLogging(SocketSettings socketSettings) : base(socketSettings) { }

        public void Execute() {
            Socket.Client.Send(toBytes(CreateCommand("enableLogging", SocketSettings.logFlag.ToString())));

            var data = Recvall();
            if (data.Equals("Ok"))
                return;

            HandleErrors(data);
        }
    }
}