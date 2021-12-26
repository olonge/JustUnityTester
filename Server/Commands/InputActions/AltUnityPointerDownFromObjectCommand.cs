using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityPointerDownFromObjectCommand : AltUnityCommand {
        TestObject altUnityObject;

        public AltUnityPointerDownFromObjectCommand(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("PointerDown object: " + altUnityObject);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            TestRunner._altUnityRunner.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
            var camera = TestRunner._altUnityRunner.FoundCameraById(altUnityObject.idCamera);
            if (camera != null) {
                response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject, camera));
            } else {
                response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject));
            }
            return response;
        }
    }
}
