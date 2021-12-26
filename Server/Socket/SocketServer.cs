using System.Collections;
using System.Net;
using System.Text;
using System.Threading;

namespace JustUnityTester.Server {
    public class SocketServer {
        protected TcpListener Listener;
        protected readonly ISocketDelegatable iClientSocketDelegatable;
        protected readonly string SeparatorString;
        protected readonly Encoding Encoding;
        protected ArrayList SocketThreads;
        protected readonly int portNumber;
        protected readonly IPEndPoint localEndPoint;
        protected readonly int maxClients;

        public int PortNumber => portNumber;

        public IPEndPoint LocalEndPoint => localEndPoint;
        public int MaxClients => maxClients;
        public int ClientCount => SocketThreads.Count;

        public bool IsServerStopped() => SocketThreads == null || (SocketThreads.Count != 0 && ((SocketThread)SocketThreads[0]).Handler.ToBeKilled);

        public SocketServer(ISocketDelegatable iClientSocketDelegatable, int portNumber = 13000, 
            int maxClients = 1, string separatorString = "\n", Encoding encoding = null) {

            this.portNumber = portNumber;
            this.iClientSocketDelegatable = iClientSocketDelegatable;
            SeparatorString = separatorString;
            Encoding = encoding ?? Encoding.UTF32;
            SocketThreads = ArrayList.Synchronized(new ArrayList());
            this.maxClients = maxClients;

            var ipAddress = IPAddress.Parse("0.0.0.0");
            localEndPoint = new IPEndPoint(ipAddress, this.portNumber);
            Listener = new TcpListener(localEndPoint.Address, this.portNumber);

            UnityEngine.Debug.Log("Created TCP listener.");
        }

        public void StartListeningForConnections() {
            foreach (SocketThread socketThread in SocketThreads) {
                UnityEngine.Debug.Log("calling stop on thread " + socketThread.Thread.ManagedThreadId);
                socketThread.Handler.Cleanup();
            }

            SocketThreads = ArrayList.Synchronized(new ArrayList());
            UnityEngine.Debug.Log("Began listening for TCP clients.");
            Listener.Start();
            ListenForConnection();
        }

        protected void ListenForConnection() => Listener.BeginAcceptTcpClient(AcceptCallback, Listener);

        // NOT on main thread
        protected void AcceptCallback(System.IAsyncResult ar) {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            UnityEngine.Debug.Log("Accept thread id: " + threadId);
            var listener = (System.Net.Sockets.TcpListener)ar.AsyncState;
            var client = listener.EndAcceptTcpClient(ar);

            UnityEngine.Debug.Log("thread id " + threadId + " accepted client " + client.Client.RemoteEndPoint);
            UnityEngine.Debug.Log("thread id " + threadId + " beginning read from client " + client.Client.RemoteEndPoint);

            var clientHandler = new ClientSocket(client, iClientSocketDelegatable, SeparatorString, Encoding);

            Thread clientThread = new Thread(clientHandler.Run);
            SocketThreads.Add(new SocketThread(clientThread, clientHandler));
            clientThread.Start();

            UnityEngine.Debug.Log("Client thread started");

            if (ClientCount < maxClients) {
                UnityEngine.Debug.Log("client handler threads less than max clients. Listening again");
                ListenForConnection();
            } else {
                UnityEngine.Debug.Log($"Max number of clients reached ({maxClients}), will not listen for any  more.");
                StopListeningForConnections();
            }
        }

        public void StopListeningForConnections() {
            Listener.Stop();
            UnityEngine.Debug.Log("Stopped listening for connections");
        }

        public void Cleanup() {
            StopListeningForConnections();

            foreach (SocketThread holder in SocketThreads) {
                UnityEngine.Debug.Log("calling stop on thread " + holder.Thread.ManagedThreadId);
                holder.Handler.Cleanup();
                UnityEngine.Debug.Log("Calling thread abort on thread: " + holder.Thread.ManagedThreadId);
                holder.Handler.ToBeKilled = true;
                holder.Thread.Abort();
            }
            SocketThreads = null;
        }

        public bool IsStarted() => Listener != null && Listener.Active;
    }
}