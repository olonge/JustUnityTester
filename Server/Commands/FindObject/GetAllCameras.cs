using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class GetAllCameras : Command {
        public override string Execute() {
            TestRunner.Instance.LogMessage("getAllCameras");
            string response = TestRunner.Instance.errorNotFoundMessage;
            var cameras = UnityEngine.Object.FindObjectsOfType<UnityEngine.Camera>();
            System.Collections.Generic.List<TestObject> cameraObjects = new System.Collections.Generic.List<TestObject>();
            foreach (UnityEngine.Camera camera in cameras) {
                cameraObjects.Add(TestRunner.Instance.GameObjectToAltUnityObject(camera.gameObject));
            }
            response = Newtonsoft.Json.JsonConvert.SerializeObject(cameraObjects);
            return response;
        }
    }
}
