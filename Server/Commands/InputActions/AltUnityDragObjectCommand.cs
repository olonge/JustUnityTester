using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityDragObjectCommand : AltUnityCommand {
        UnityEngine.Vector2 position;
        TestObject altUnityObject;

        public AltUnityDragObjectCommand(UnityEngine.Vector2 position, TestObject altUnityObject) {
            this.position = position;
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("Drag object: " + altUnityObject);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            AltUnityMockUpPointerInputModule mockUp = new AltUnityMockUpPointerInputModule();
            var pointerEventData = mockUp.ExecuteTouchEvent(new UnityEngine.Touch() { position = position });
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            UnityEngine.Camera viewingCamera = TestRunner._altUnityRunner.FoundCameraById(altUnityObject.idCamera);
            UnityEngine.Vector3 gameObjectPosition = viewingCamera.WorldToScreenPoint(gameObject.transform.position);
            pointerEventData.delta = pointerEventData.position - new UnityEngine.Vector2(gameObjectPosition.x, gameObjectPosition.y);
            TestRunner._altUnityRunner.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.dragHandler);
            var camera = TestRunner._altUnityRunner.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(camera != null ? TestRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject, camera) : TestRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject));
            return response;
        }
    }
}
