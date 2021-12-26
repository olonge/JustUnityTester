using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnitySetObjectComponentPropertyCommand : AltUnityReflectionMethodsCommand {
        string altObjectString;
        string propertyString;
        string valueString;

        public AltUnitySetObjectComponentPropertyCommand(string altObjectString, string propertyString, string valueString) {
            this.altObjectString = altObjectString;
            this.propertyString = propertyString;
            this.valueString = valueString;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("set property " + propertyString + " to value: " + valueString + " for object " + altObjectString);
            string response = TestRunner.Instance.errorPropertyNotFoundMessage;
            TestObjectProperty altProperty =
                Newtonsoft.Json.JsonConvert.DeserializeObject<TestObjectProperty>(propertyString);
            TestObject altUnityObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(altObjectString);
            UnityEngine.GameObject testableObject = TestRunner.GetGameObject(altUnityObject);
            System.Reflection.MemberInfo memberInfo = GetMemberForObjectComponent(altUnityObject, altProperty);
            response = SetValueForMember(memberInfo, valueString, testableObject, altProperty);
            return response;
        }
    }
}
