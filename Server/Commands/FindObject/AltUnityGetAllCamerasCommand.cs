using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetAllCamerasCommand : AltUnityCommand {
        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("getAllCameras");
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            var cameras = UnityEngine.Object.FindObjectsOfType<UnityEngine.Camera>();
            System.Collections.Generic.List<TestObject> cameraObjects = new System.Collections.Generic.List<TestObject>();
            foreach (UnityEngine.Camera camera in cameras) {
                cameraObjects.Add(TestRunner._altUnityRunner.GameObjectToAltUnityObject(camera.gameObject));
            }
            response = Newtonsoft.Json.JsonConvert.SerializeObject(cameraObjects);
            return response;
        }
    }
}
