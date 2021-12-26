using System.Linq;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindActiveObjectsByNameCommand : AltUnityBaseClassFindObjectsCommand {
        string methodParameters;

        public AltUnityFindActiveObjectsByNameCommand(string stringSent) {
            methodParameters = stringSent;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner._altUnityRunner.LogMessage("findActiveObjectByName for: " + objectName);
            By cameraBy = (By)System.Enum.Parse(typeof(By), pieces[1]);
            string cameraPath = pieces[2];
            bool enabled = System.Convert.ToBoolean(pieces[3]);

            string response = TestRunner._altUnityRunner.errorNotFoundMessage;

            var foundGameObject = UnityEngine.GameObject.Find(objectName);
            if (foundGameObject != null) {

                UnityEngine.Camera camera = null;
                if (!cameraPath.Equals("//")) {
                    camera = GetCamera(cameraBy, cameraPath);
                    if (camera == null)
                        return TestRunner._altUnityRunner.errorCameraNotFound;
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(foundGameObject, camera));

            }
            return response;

        }
    }
}
