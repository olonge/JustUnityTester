
using System.Linq;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectByNameCommand : AltUnityFindObjectsOldWayCommand {
        string methodParameters;

        public AltUnityFindObjectByNameCommand(string methodParameters) {
            this.methodParameters = methodParameters;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner.Instance.LogMessage("find object by name " + objectName);
            string cameraName = pieces[1];
            bool enabled = System.Convert.ToBoolean(pieces[2]);
            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.GameObject foundGameObject = FindObjectInScene(objectName, enabled);
            if (foundGameObject != null) {
                if (cameraName.Equals(""))
                    response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(foundGameObject));
                else {
                    UnityEngine.Camera camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
                    response = camera == null ? TestRunner.Instance.errorNotFoundMessage : Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(foundGameObject, camera));
                }
            }
            return response;
        }
    }
}
