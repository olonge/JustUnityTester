using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class PointerDownFromObject : Command {
        TestObject altUnityObject;

        public PointerDownFromObject(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("PointerDown object: " + altUnityObject);
            string response = TestRunner.Instance.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            TestRunner.Instance.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
            var camera = TestRunner.Instance.FoundCameraById(altUnityObject.idCamera);
            if (camera != null) {
                response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(gameObject, camera));
            } else {
                response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(gameObject));
            }
            return response;
        }
    }
}
