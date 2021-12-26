using System.Linq;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectWhereNameContainsCommand : AltUnityCommand {
        string methodParameters;

        public AltUnityFindObjectWhereNameContainsCommand(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner._altUnityRunner.LogMessage("find object where name contains:" + objectName);
            string cameraName = pieces[1];
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                if (testableObject.name.Contains(objectName)) {
                    response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(testableObject, camera));
                    break;
                }
            }
            return response;
        }
    }
}
