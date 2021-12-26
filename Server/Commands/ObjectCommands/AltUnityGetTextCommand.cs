using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetTextCommand : AltUnityReflectionMethodsCommand {
        static readonly AltUnityObjectProperty[] TextProperties =
        {
            new AltUnityObjectProperty("UnityEngine.UI.Text", "text"),
            new AltUnityObjectProperty("UnityEngine.UI.InputField", "text"),
            new AltUnityObjectProperty("TMPro.TMP_Text", "text", "Unity.TextMeshPro"),
            new AltUnityObjectProperty("TMPro.TMP_InputField", "text", "Unity.TextMeshPro")
        };

        TestObject altUnityObject;

        public AltUnityGetTextCommand(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("Get text from object by name " + altUnityObject.name);
            var response = AltUnityRunner._altUnityRunner.errorPropertyNotFoundMessage;

            var targetObject = AltUnityRunner.GetGameObject(altUnityObject);

            foreach (var property in TextProperties) {
                try {
                    var memberInfo = GetMemberForObjectComponent(altUnityObject, property);
                    response = GetValueForMember(memberInfo, targetObject, property);
                    if (!response.Contains("error:"))
                        break;
                } catch (Exceptions.PropertyNotFoundException) {
                    response = AltUnityRunner._altUnityRunner.errorPropertyNotFoundMessage;
                } catch (Exceptions.ComponentNotFoundException) {
                    response = AltUnityRunner._altUnityRunner.errorComponentNotFoundMessage;
                }
            }

            return response;
        }
    }
}