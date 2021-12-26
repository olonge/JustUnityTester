namespace JustUnityTester.Server {
    public class AltTcpListener : System.Net.Sockets.TcpListener {
        public AltTcpListener(System.Net.IPEndPoint localEp) : base(localEp) {
        }

        public AltTcpListener(System.Net.IPAddress localaddr, int port) : base(localaddr, port) {
        }

        public new bool Active {
            get { return base.Active; }
        }
    }
}