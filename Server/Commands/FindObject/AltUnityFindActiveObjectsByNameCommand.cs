using System.Linq;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindActiveObjectsByNameCommand : AltUnityBaseClassFindObjectsCommand {
        string methodParameters;

        public AltUnityFindActiveObjectsByNameCommand(string stringSent) {
            methodParameters = stringSent;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { AltUnityRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            AltUnityRunner._altUnityRunner.LogMessage("findActiveObjectByName for: " + objectName);
            By cameraBy = (By)System.Enum.Parse(typeof(By), pieces[1]);
            string cameraPath = pieces[2];
            bool enabled = System.Convert.ToBoolean(pieces[3]);

            string response = AltUnityRunner._altUnityRunner.errorNotFoundMessage;

            var foundGameObject = UnityEngine.GameObject.Find(objectName);
            if (foundGameObject != null) {

                UnityEngine.Camera camera = null;
                if (!cameraPath.Equals("//")) {
                    camera = GetCamera(cameraBy, cameraPath);
                    if (camera == null)
                        return AltUnityRunner._altUnityRunner.errorCameraNotFound;
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(AltUnityRunner._altUnityRunner.GameObjectToAltUnityObject(foundGameObject, camera));

            }
            return response;

        }
    }
}
