using System.Collections.Generic;
using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityGetAllMethods : BaseCommand {
        TestComponent testComponent;
        TestObject altUnityObject;
        TestMethodSelection methodSelection;

        public AltUnityGetAllMethods(SocketSettings socketSettings, TestComponent altUnityComponent, TestObject altUnityObject, TestMethodSelection methodSelection = TestMethodSelection.ALLMETHODS) : base(socketSettings) {
            this.testComponent = altUnityComponent;
            this.altUnityObject = altUnityObject;
            this.methodSelection = methodSelection;
        }
        public List<string> Execute() {
            var altComponent = Newtonsoft.Json.JsonConvert.SerializeObject(testComponent);
            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("getAllMethods", altComponent, methodSelection.ToString())));
            string data = Recvall();

            if (!data.Contains("error:"))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(data);

            HandleErrors(data);
            return null;
        }


    }
}