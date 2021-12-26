using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class GetComponentProperty : ReflectionMethods {
        string altObjectString;
        string propertyString;

        public GetComponentProperty(string altObjectString, string propertyString) {
            this.altObjectString = altObjectString;
            this.propertyString = propertyString;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("get property " + propertyString + " for object " + altObjectString);
            string response = TestRunner.Instance.errorPropertyNotFoundMessage;
            TestObjectProperty altProperty = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObjectProperty>(propertyString);
            TestObject altUnityObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TestObject>(altObjectString);
            UnityEngine.GameObject testableObject = TestRunner.GetGameObject(altUnityObject);
            System.Reflection.MemberInfo memberInfo = GetMemberForObjectComponent(altUnityObject, altProperty);
            response = GetValueForMember(memberInfo, testableObject, altProperty);
            return response;

        }
    }
}
