using System.Collections.Generic;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Driver.Commands {
    public class ReturnedElement : BaseCommand {
        public ReturnedElement(SocketSettings socketSettings) : base(socketSettings) { }

        protected TestObject ReceiveAltUnityObject() {
            string data = Recvall();

            if (!data.Contains("error:")) {
                var testElement = JsonConvert.DeserializeObject<TestObject>(data);
                testElement.socketSettings = SocketSettings;
                return testElement;
            }

            HandleErrors(data);
            return null;
        }

        protected List<TestObject> ReceiveListOfAltUnityObjects() {
            string data = Recvall();
            if (!data.Contains("error:")) {
                var altElements = JsonConvert.DeserializeObject<List<TestObject>>(data);
                foreach (var altElement in altElements)
                    altElement.socketSettings = SocketSettings;
                return altElements;
            }

            HandleErrors(data);
            return null;
        }
    }
}