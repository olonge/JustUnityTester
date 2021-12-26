using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class DragObject : AltUnityCommand {
        UnityEngine.Vector2 position;
        TestObject altUnityObject;

        public DragObject(UnityEngine.Vector2 position, TestObject altUnityObject) {
            this.position = position;
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("Drag object: " + altUnityObject);
            string response = TestRunner.Instance.errorNotFoundMessage;
            MockUpPointerInputModule mockUp = new MockUpPointerInputModule();
            var pointerEventData = mockUp.ExecuteTouchEvent(new UnityEngine.Touch() { position = position });
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            UnityEngine.Camera viewingCamera = TestRunner.Instance.FoundCameraById(altUnityObject.idCamera);
            UnityEngine.Vector3 gameObjectPosition = viewingCamera.WorldToScreenPoint(gameObject.transform.position);
            pointerEventData.delta = pointerEventData.position - new UnityEngine.Vector2(gameObjectPosition.x, gameObjectPosition.y);
            TestRunner.Instance.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.dragHandler);
            var camera = TestRunner.Instance.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(camera != null ? TestRunner.Instance.GameObjectToAltUnityObject(gameObject, camera) : TestRunner.Instance.GameObjectToAltUnityObject(gameObject));
            return response;
        }
    }
}
