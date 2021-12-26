using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class SetComponentProperty : BaseCommand {
        string componentName;
        string propertyName;
        string value;
        string assemblyName;
        TestObject altUnityObject;

        public SetComponentProperty(SocketSettings socketSettings, string componentName, string propertyName, string value, string assemblyName, TestObject altUnityObject) : base(socketSettings) {
            this.componentName = componentName;
            this.propertyName = propertyName;
            this.value = value;
            this.assemblyName = assemblyName;
            this.altUnityObject = altUnityObject;
        }
        public string Execute() {
            string altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            string propertyInfo = Newtonsoft.Json.JsonConvert.SerializeObject(new TestObjectProperty(componentName, propertyName, assemblyName));
            Socket.Client.Send(
                System.Text.Encoding.ASCII.GetBytes(CreateCommand("setObjectComponentProperty", altObject, propertyInfo, value)));
            string data = Recvall();
            if (!data.Contains("error:")) return data;
            HandleErrors(data);
            return null;
        }
    }
}