using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityCallComponentMethod : BaseCommand {
        string componentName;
        string methodName;
        string parameters;
        string typeOfParameters;
        string assemblyName;
        TestObject altUnityObject;
        public AltUnityCallComponentMethod(SocketSettings socketSettings, string componentName, string methodName, string parameters, string typeOfParameters, string assembly, TestObject altUnityObject) : base(socketSettings) {
            this.componentName = componentName;
            this.methodName = methodName;
            this.parameters = parameters;
            this.typeOfParameters = typeOfParameters;
            assemblyName = assembly;
            this.altUnityObject = altUnityObject;
        }
        public string Execute() {
            string altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            string actionInfo =
                Newtonsoft.Json.JsonConvert.SerializeObject(new TestObjectAction(componentName, methodName, parameters, typeOfParameters, assemblyName));
            Socket.Client.Send(
                 System.Text.Encoding.ASCII.GetBytes(CreateCommand("callComponentMethodForObject", altObject, actionInfo)));
            string data = Recvall();
            if (!data.Contains("error:")) return data;
            HandleErrors(data);
            return null;

        }
    }
}