using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class Tap : ReturnedElement {
        TestObject altUnityObject;
        int count;

        public Tap(SocketSettings socketSettings, TestObject altUnityObject, int count) : base(socketSettings) {
            this.altUnityObject = altUnityObject;
            this.count = count;
        }

        public TestObject Execute() {
            var altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("tapObject", altObject, count.ToString())));
            return ReceiveAltUnityObject();
        }
    }
}