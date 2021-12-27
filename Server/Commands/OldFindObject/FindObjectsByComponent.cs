using System.Linq;
using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class FindObjectsByComponent : ReflectionMethods {
        string methodParameters;

        public FindObjectsByComponent(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string assemblyName = pieces[0];
            string componentTypeName = pieces[1];
            TestRunner.Instance.LogMessage("find objects by component " + componentTypeName);
            string cameraName = pieces[2];
            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            System.Collections.Generic.List<TestObject> foundObjects = new System.Collections.Generic.List<TestObject>();
            System.Type componentType = GetType(componentTypeName, assemblyName);
            if (componentType != null) {
                foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                    if (testableObject.GetComponent(componentType) != null) {
                        foundObjects.Add(TestRunner.Instance.GameObjectToAltUnityObject(testableObject, camera));
                    }
                }

                response = Newtonsoft.Json.JsonConvert.SerializeObject(foundObjects);
            } else {
                response = TestRunner.Instance.errorComponentNotFoundMessage;
            }
            return response;
        }
    }
}