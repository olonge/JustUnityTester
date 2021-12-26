using System.Threading;

namespace JustUnityTester.Server {
    public class SocketThread {
        protected readonly Thread thread;
        protected readonly ClientSocket handler;

        public Thread Thread => thread;

        public ClientSocket Handler => handler;

        public SocketThread(Thread thread, ClientSocket handler) {
            this.thread = thread;
            this.handler = handler;
        }
    }
}