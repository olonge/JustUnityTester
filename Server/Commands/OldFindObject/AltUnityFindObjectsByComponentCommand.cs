using System.Linq;
using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectsByComponentCommand : AltUnityReflectionMethodsCommand {
        string methodParameters;

        public AltUnityFindObjectsByComponentCommand(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { AltUnityRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string assemblyName = pieces[0];
            string componentTypeName = pieces[1];
            AltUnityRunner._altUnityRunner.LogMessage("find objects by component " + componentTypeName);
            string cameraName = pieces[2];
            string response = AltUnityRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null) {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            System.Collections.Generic.List<AltUnityObject> foundObjects = new System.Collections.Generic.List<AltUnityObject>();
            System.Type componentType = GetType(componentTypeName, assemblyName);
            if (componentType != null) {
                foreach (UnityEngine.GameObject testableObject in UnityEngine.Object.FindObjectsOfType<UnityEngine.GameObject>()) {
                    if (testableObject.GetComponent(componentType) != null) {
                        foundObjects.Add(AltUnityRunner._altUnityRunner.GameObjectToAltUnityObject(testableObject, camera));
                    }
                }

                response = Newtonsoft.Json.JsonConvert.SerializeObject(foundObjects);
            } else {
                response = AltUnityRunner._altUnityRunner.errorComponentNotFoundMessage;
            }
            return response;
        }
    }
}
