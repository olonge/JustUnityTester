
using System.Linq;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectByNameCommand : AltUnityFindObjectsOldWayCommand {
        string methodParameters;

        public AltUnityFindObjectByNameCommand(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner._altUnityRunner.LogMessage("find object by name " + objectName);
            string cameraName = pieces[1];
            bool enabled = System.Convert.ToBoolean(pieces[2]);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.GameObject foundGameObject = FindObjectInScene(objectName, enabled);
            if (foundGameObject != null) {
                if (cameraName.Equals(""))
                    response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(foundGameObject));
                else {
                    UnityEngine.Camera camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
                    response = camera == null ? TestRunner._altUnityRunner.errorNotFoundMessage : Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(foundGameObject, camera));
                }
            }
            return response;
        }
    }
}
