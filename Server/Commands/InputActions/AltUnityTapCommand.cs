using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityTapCommand : AltUnityCommand {
        TestObject altUnityObject;
        int count;

        public AltUnityTapCommand(TestObject altUnityObject, int count) {
            this.altUnityObject = altUnityObject;
            this.count = count;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("tapped object by name " + altUnityObject.name);
            TestRunner.Instance.ShowClick(new UnityEngine.Vector2(altUnityObject.getScreenPosition().x, altUnityObject.getScreenPosition().y));
            var response = TestRunner.Instance.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            TestRunner.Instance.LogMessage("GameObject: " + gameObject);

            UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
            gameObject.SendMessage("OnMouseEnter", UnityEngine.SendMessageOptions.DontRequireReceiver);

            for (var i = 0; i < count; i++)
                InitiateClick(gameObject, pointerEventData);

            UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
            gameObject.SendMessage("OnMouseExit", UnityEngine.SendMessageOptions.DontRequireReceiver);

            var camera = TestRunner.Instance.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(gameObject, camera));

            return response;
        }

        private void InitiateClick(UnityEngine.GameObject gameObject, UnityEngine.EventSystems.PointerEventData pointerEventData) {
            pointerEventData.clickTime = UnityEngine.Time.unscaledTime;

            UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
            gameObject.SendMessage("OnMouseDown", UnityEngine.SendMessageOptions.DontRequireReceiver);
            UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.initializePotentialDrag);
            gameObject.SendMessage("OnMouseOver", UnityEngine.SendMessageOptions.DontRequireReceiver);
            UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
            gameObject.SendMessage("OnMouseUp", UnityEngine.SendMessageOptions.DontRequireReceiver);
            UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
            gameObject.SendMessage("OnMouseUpAsButton", UnityEngine.SendMessageOptions.DontRequireReceiver);
        }
    }
}
