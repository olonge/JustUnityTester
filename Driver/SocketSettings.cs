namespace JustUnityTester {
    public struct SocketSettings {
        public System.Net.Sockets.TcpClient socket;
        public string requestSeparator;
        public string requestEnding;
        public bool logFlag;

        public SocketSettings(System.Net.Sockets.TcpClient socket, string requestSeparator, string requestEnding, bool logFlag) {
            this.socket = socket;
            this.requestSeparator = requestSeparator;
            this.requestEnding = requestEnding;
            this.logFlag = logFlag;
        }
    }    
}
