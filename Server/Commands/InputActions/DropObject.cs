using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class DropObject : Command {
        UnityEngine.Vector2 position;
        TestObject altUnityObject;

        public DropObject(UnityEngine.Vector2 position, TestObject altUnityObject) {
            this.position = position;
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("Drop object: " + altUnityObject);
            string response = TestRunner.Instance.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            TestRunner.Instance.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.dropHandler);
            var camera = TestRunner.Instance.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(camera != null ? TestRunner.Instance.GameObjectToAltUnityObject(gameObject, camera) : TestRunner.Instance.GameObjectToAltUnityObject(gameObject));
            return response;
        }
    }
}
