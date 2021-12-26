using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityPointerEnterObjectCommand : AltUnityCommand {
        TestObject altUnityObject;

        public AltUnityPointerEnterObjectCommand(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("PointerEnter object: " + altUnityObject);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            TestRunner._altUnityRunner.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
            var camera = TestRunner._altUnityRunner.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(camera != null ? TestRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject, camera) : TestRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject));
            return response;
        }
    }
}
