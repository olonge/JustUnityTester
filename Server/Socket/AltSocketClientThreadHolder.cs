namespace JustUnityTester.Server {
    public class AltSocketClientThreadHolder {
        protected readonly System.Threading.Thread thread;
        protected readonly AltClientSocketHandler handler;

        public System.Threading.Thread Thread {
            get {
                return thread;
            }
        }

        public AltClientSocketHandler Handler {
            get {
                return handler;
            }
        }

        public AltSocketClientThreadHolder(System.Threading.Thread thread, AltClientSocketHandler handler) {
            this.thread = thread;
            this.handler = handler;
        }
    }
}