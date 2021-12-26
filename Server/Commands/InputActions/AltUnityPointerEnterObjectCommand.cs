using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityPointerEnterObjectCommand : AltUnityCommand {
        TestObject altUnityObject;

        public AltUnityPointerEnterObjectCommand(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("PointerEnter object: " + altUnityObject);
            string response = AltUnityRunner._altUnityRunner.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = AltUnityRunner.GetGameObject(altUnityObject);
            AltUnityRunner._altUnityRunner.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
            var camera = AltUnityRunner._altUnityRunner.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(camera != null ? AltUnityRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject, camera) : AltUnityRunner._altUnityRunner.GameObjectToAltUnityObject(gameObject));
            return response;
        }
    }
}
