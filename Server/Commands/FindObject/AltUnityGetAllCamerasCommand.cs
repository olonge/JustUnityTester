using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetAllCamerasCommand : AltUnityCommand {
        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("getAllCameras");
            string response = AltUnityRunner._altUnityRunner.errorNotFoundMessage;
            var cameras = UnityEngine.Object.FindObjectsOfType<UnityEngine.Camera>();
            System.Collections.Generic.List<AltUnityObject> cameraObjects = new System.Collections.Generic.List<AltUnityObject>();
            foreach (UnityEngine.Camera camera in cameras) {
                cameraObjects.Add(AltUnityRunner._altUnityRunner.GameObjectToAltUnityObject(camera.gameObject));
            }
            response = Newtonsoft.Json.JsonConvert.SerializeObject(cameraObjects);
            return response;
        }
    }
}
