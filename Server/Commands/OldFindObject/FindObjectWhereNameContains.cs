using System.Linq;

namespace JustUnityTester.Server.Commands {
    class FindObjectWhereNameContains : Command {
        string methodParameters;

        public FindObjectWhereNameContains(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner.Instance.LogMessage("find object where name contains:" + objectName);
            string cameraName = pieces[1];
            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                if (testableObject.name.Contains(objectName)) {
                    response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(testableObject, camera));
                    break;
                }
            }
            return response;
        }
    }
}
