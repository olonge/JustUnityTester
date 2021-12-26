using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnitySetTextCommand : AltUnityReflectionMethodsCommand {
        static readonly TestObjectProperty[] TextProperties =
        {
            new TestObjectProperty("UnityEngine.UI.Text", "text"),
            new TestObjectProperty("UnityEngine.UI.InputField", "text"),
            new TestObjectProperty("TMPro.TMP_Text", "text", "Unity.TextMeshPro"),
            new TestObjectProperty("TMPro.TMP_InputField", "text", "Unity.TextMeshPro")
        };

        TestObject altUnityObject;
        string inputText;

        public AltUnitySetTextCommand(TestObject altUnityObject, string text) {
            this.altUnityObject = altUnityObject;
            inputText = text;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("Set text for object by name " + altUnityObject.name);
            var response = TestRunner.Instance.errorNotFoundMessage;

            var targetObject = TestRunner.GetGameObject(altUnityObject);

            foreach (var property in TextProperties) {
                try {
                    var memberInfo = GetMemberForObjectComponent(altUnityObject, property);
                    response = SetValueForMember(memberInfo, inputText, targetObject, property);
                    if (!response.Contains("error:"))
                        return Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(targetObject));
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