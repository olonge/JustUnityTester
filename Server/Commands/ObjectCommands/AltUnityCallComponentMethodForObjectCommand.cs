using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityCallComponentMethodForObjectCommand : AltUnityReflectionMethodsCommand {
        string altObjectString;
        string actionString;

        public AltUnityCallComponentMethodForObjectCommand(string altObjectString, string actionString) {
            this.altObjectString = altObjectString;
            this.actionString = actionString;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("call action " + actionString + " for object " + altObjectString);
            string response = TestRunner._altUnityRunner.errorMethodNotFoundMessage;
            System.Reflection.MethodInfo methodInfoToBeInvoked;
            TestObjectAction altAction = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObjectAction>(actionString);
            var componentType = GetType(altAction.Component, altAction.Assembly);

            System.Reflection.MethodInfo[] methodInfos = GetMethodInfoWithSpecificName(componentType, altAction.Method);
            if (methodInfos.Length == 1)
                methodInfoToBeInvoked = methodInfos[0];
            else {
                methodInfoToBeInvoked = GetMethodToBeInvoked(methodInfos, altAction);
            }



            if (string.IsNullOrEmpty(altObjectString)) {
                response = InvokeMethod(methodInfoToBeInvoked, altAction, null, response);
            } else {
                TestObject altObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(altObjectString);
                UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altObject);
                if (componentType == typeof(UnityEngine.GameObject)) {
                    response = InvokeMethod(methodInfoToBeInvoked, altAction, gameObject, response);
                } else
                if (gameObject.GetComponent(componentType) != null) {
                    UnityEngine.Component component = gameObject.GetComponent(componentType);
                    response = InvokeMethod(methodInfoToBeInvoked, altAction, component, response);
                }
            }
            return response;

        }
    }
}
