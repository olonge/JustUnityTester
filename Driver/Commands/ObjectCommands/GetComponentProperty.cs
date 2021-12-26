using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class GetComponentProperty : BaseCommand {
        string componentName;
        string propertyName;
        string assemblyName;
        TestObject altUnityObject;
        public GetComponentProperty(SocketSettings socketSettings, string componentName, string propertyName, string assemblyName, TestObject altUnityObject) : base(socketSettings) {
            this.componentName = componentName;
            this.propertyName = propertyName;
            this.assemblyName = assemblyName;
            this.altUnityObject = altUnityObject;
        }
        public string Execute() {
            string altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            string propertyInfo = Newtonsoft.Json.JsonConvert.SerializeObject(new TestObjectProperty(componentName, propertyName, assemblyName));
            Socket.Client.Send(
                 System.Text.Encoding.ASCII.GetBytes(CreateCommand("getObjectComponentProperty", altObject, propertyInfo)));
            string data = Recvall();
            if (!data.Contains("error:")) return data;
            HandleErrors(data);
            return null;
        }
    }
}