using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetComponentPropertyCommand : AltUnityReflectionMethodsCommand {
        string altObjectString;
        string propertyString;

        public AltUnityGetComponentPropertyCommand(string altObjectString, string propertyString) {
            this.altObjectString = altObjectString;
            this.propertyString = propertyString;
        }

        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("get property " + propertyString + " for object " + altObjectString);
            string response = AltUnityRunner._altUnityRunner.errorPropertyNotFoundMessage;
            TestObjectProperty altProperty = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObjectProperty>(propertyString);
            TestObject altUnityObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(altObjectString);
            UnityEngine.GameObject testableObject = AltUnityRunner.GetGameObject(altUnityObject);
            System.Reflection.MemberInfo memberInfo = GetMemberForObjectComponent(altUnityObject, altProperty);
            response = GetValueForMember(memberInfo, testableObject, altProperty);
            return response;

        }
    }
}