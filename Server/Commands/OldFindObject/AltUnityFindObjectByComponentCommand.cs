using System.Linq;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectByComponentCommand : AltUnityReflectionMethodsCommand {
        string methodParameters;

        public AltUnityFindObjectByComponentCommand(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string assemblyName = pieces[0];
            string componentTypeName = pieces[1];
            TestRunner._altUnityRunner.LogMessage("find object by component " + componentTypeName);
            string cameraName = pieces[2];
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            System.Type componentType = GetType(componentTypeName, assemblyName);
            if (componentType != null) {
                foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                    if (testableObject.GetComponent(componentType) != null) {
                        var foundObject = testableObject;
                        response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(foundObject, camera));
                        break;
                    }
                }
            } else {
                response = TestRunner._altUnityRunner.errorComponentNotFoundMessage;
            }
            return response;
        }
    }
}
