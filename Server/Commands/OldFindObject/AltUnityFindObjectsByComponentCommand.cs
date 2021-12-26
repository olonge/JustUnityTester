using System.Linq;
using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectsByComponentCommand : AltUnityReflectionMethodsCommand {
        string methodParameters;

        public AltUnityFindObjectsByComponentCommand(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string assemblyName = pieces[0];
            string componentTypeName = pieces[1];
            TestRunner._altUnityRunner.LogMessage("find objects by component " + componentTypeName);
            string cameraName = pieces[2];
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            System.Collections.Generic.List<TestObject> foundObjects = new System.Collections.Generic.List<TestObject>();
            System.Type componentType = GetType(componentTypeName, assemblyName);
            if (componentType != null) {
                foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                    if (testableObject.GetComponent(componentType) != null) {
                        foundObjects.Add(TestRunner._altUnityRunner.GameObjectToAltUnityObject(testableObject, camera));
                    }
                }

                response = Newtonsoft.Json.JsonConvert.SerializeObject(foundObjects);
            } else {
                response = TestRunner._altUnityRunner.errorComponentNotFoundMessage;
            }
            return response;
        }
    }
}
