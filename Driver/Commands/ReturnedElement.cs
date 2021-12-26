using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class ReturnedElement : BaseCommand {
        public ReturnedElement(SocketSettings socketSettings) : base(socketSettings) {
        }

        protected TestObject ReceiveAltUnityObject() {
            string data = Recvall();
            if (!data.Contains("error:")) {
                TestObject altElement = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(data);
                altElement.socketSettings = SocketSettings;
                return altElement;
            }
            HandleErrors(data);
            return null;
        }
        protected System.Collections.Generic.List<TestObject> ReceiveListOfAltUnityObjects() {
            string data = Recvall();
            if (!data.Contains("error:")) {
                var altElements = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<TestObject>>(data);
                foreach (var altElement in altElements) {
                    altElement.socketSettings = SocketSettings;
                }
                return altElements;
            }
            HandleErrors(data);
            return null;
        }

    }
}