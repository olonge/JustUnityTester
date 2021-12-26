using System.Net;

namespace JustUnityTester.Server {
    public class TcpListener : System.Net.Sockets.TcpListener {
        public TcpListener(IPEndPoint localEp) : base(localEp) {
        }

        public TcpListener(IPAddress localaddr, int port) : base(localaddr, port) {
        }

        public new bool Active => base.Active;
    }
}