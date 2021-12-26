using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityClickEventCommand : AltUnityCommand {
        TestObject altUnityObject;

        public AltUnityClickEventCommand(TestObject altObject) {
            altUnityObject = altObject;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("ClickEvent on " + altUnityObject);
            TestRunner.Instance.ShowClick(new UnityEngine.Vector2(altUnityObject.getScreenPosition().x, altUnityObject.getScreenPosition().y));

            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.GameObject foundGameObject = TestRunner.GetGameObject(altUnityObject);
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.EventSystems.ExecuteEvents.Execute(foundGameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(foundGameObject));
            return response;
        }
    }
}
