using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class GetText : ReflectionMethods {
        static readonly TestObjectProperty[] TextProperties =
        {
            new TestObjectProperty("UnityEngine.UI.Text", "text"),
            new TestObjectProperty("UnityEngine.UI.InputField", "text"),
            new TestObjectProperty("TMPro.TMP_Text", "text", "Unity.TextMeshPro"),
            new TestObjectProperty("TMPro.TMP_InputField", "text", "Unity.TextMeshPro")
        };

        TestObject altUnityObject;

        public GetText(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("Get text from object by name " + altUnityObject.name);
            var response = TestRunner.Instance.errorPropertyNotFoundMessage;

            var targetObject = TestRunner.GetGameObject(altUnityObject);

            foreach (var property in TextProperties) {
                try {
                    var memberInfo = GetMemberForObjectComponent(altUnityObject, property);
                    response = GetValueForMember(memberInfo, targetObject, property);
                    if (!response.Contains("error:"))
                        break;
                } catch (Exceptions.PropertyNotFoundException) {
                    response = TestRunner.Instance.errorPropertyNotFoundMessage;
                } catch (Exceptions.ComponentNotFoundException) {
                    response = TestRunner.Instance.errorComponentNotFoundMessage;
                }
            }

            return response;
        }
    }
}