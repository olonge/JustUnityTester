using System.Collections.Generic;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Driver.Commands {
    public class GetAllComponents : BaseCommand {
        TestObject AltUnityObject;

        public GetAllComponents(SocketSettings socketSettings, TestObject altUnityObject) : base(socketSettings) {
            AltUnityObject = altUnityObject;
        }
        public List<TestComponent> Execute() {
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("getAllComponents", AltUnityObject.id.ToString())));
            string data = Recvall();

            if (!data.Contains("error:")) 
                return JsonConvert.DeserializeObject<List<TestComponent>>(data);

            HandleErrors(data);
            return null;
        }
    }
}