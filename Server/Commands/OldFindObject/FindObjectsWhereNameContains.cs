using System.Linq;
using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class FindObjectsWhereNameContains : Command {
        string methodParameters;

        public FindObjectsWhereNameContains(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner.Instance.LogMessage("find objects where name contains:" + objectName);
            string cameraName = pieces[1];
            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }

            System.Collections.Generic.List<TestObject> foundObjects = new System.Collections.Generic.List<TestObject>();
            foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                if (testableObject.name.Contains(objectName)) {
                    foundObjects.Add(TestRunner.Instance.GameObjectToAltUnityObject(testableObject, camera));
                }
            }

            response = Newtonsoft.Json.JsonConvert.SerializeObject(foundObjects);

            return response;
        }
    }
}
