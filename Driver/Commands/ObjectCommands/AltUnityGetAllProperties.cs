using System.Collections.Generic;
using System.Text;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityGetAllProperties : AltBaseCommand {
        TestComponent testComponent;
        TestObject altUnityObject;
        public AltUnityGetAllProperties(SocketSettings socketSettings, TestComponent altUnityComponent, TestObject altUnityObject) : base(socketSettings) {
            this.testComponent = altUnityComponent;
            this.altUnityObject = altUnityObject;
        }
        public List<AltUnityProperty> Execute() {
            var altComponent = JsonConvert.SerializeObject(testComponent);
            Socket.Client.Send(Encoding.ASCII.GetBytes(CreateCommand("getAllFields", altUnityObject.id.ToString(), altComponent)));
            string data = Recvall();

            if (!data.Contains("error:"))
                return JsonConvert.DeserializeObject<List<AltUnityProperty>>(data);

            HandleErrors(data);
            return null;
        }
    }
}