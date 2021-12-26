using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class PointerExitObject : Command {
        TestObject altUnityObject;

        public PointerExitObject(TestObject altUnityObject) {
            this.altUnityObject = altUnityObject;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("PointerExit object: " + altUnityObject);
            string response = TestRunner.Instance.errorNotFoundMessage;
            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.GameObject gameObject = TestRunner.GetGameObject(altUnityObject);
            TestRunner.Instance.LogMessage("GameOBject: " + gameObject);
            UnityEngine.EventSystems.ExecuteEvents.Execute(gameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
            var camera = TestRunner.Instance.FoundCameraById(altUnityObject.idCamera);
            response = Newtonsoft.Json.JsonConvert.SerializeObject(camera != null ? TestRunner.Instance.GameObjectToAltUnityObject(gameObject, camera) : TestRunner.Instance.GameObjectToAltUnityObject(gameObject));
            return response;
        }
    }
}
