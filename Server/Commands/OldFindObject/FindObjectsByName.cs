using System.Linq;
using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class FindObjectsByName : FindObjects_Old {
        string methodParameters;

        public FindObjectsByName(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner.Instance.LogMessage("find multiple objects by name " + objectName);
            string cameraName = pieces[1];
            bool enabled = System.Convert.ToBoolean(pieces[2]);

            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            string response = TestRunner.Instance.errorNotFoundMessage;
            System.Collections.Generic.List<TestObject> foundObjects = new System.Collections.Generic.List<TestObject>();
            foreach (UnityEngine.GameObject testableObject in FindObjectsInScene(objectName, enabled)) {
                foundObjects.Add(TestRunner.Instance.GameObjectToAltUnityObject(testableObject, camera));
            }

            response = Newtonsoft.Json.JsonConvert.SerializeObject(foundObjects);

            return response;

        }
    }
}
