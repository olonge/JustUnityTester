using System.Linq;

namespace JustUnityTester.Server.Commands {
    class FindActiveObjectsByName : BaseFindObjects {
        string methodParameters;

        public FindActiveObjectsByName(string stringSent) {
            methodParameters = stringSent;
        }

        public override string Execute() {
            var pieces = methodParameters.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner.Instance.LogMessage("findActiveObjectByName for: " + objectName);
            By cameraBy = (By)System.Enum.Parse(typeof(By), pieces[1]);
            string cameraPath = pieces[2];
            bool enabled = System.Convert.ToBoolean(pieces[3]);

            string response = TestRunner.Instance.errorNotFoundMessage;

            var foundGameObject = UnityEngine.GameObject.Find(objectName);
            if (foundGameObject != null) {

                UnityEngine.Camera camera = null;
                if (!cameraPath.Equals("//")) {
                    camera = GetCamera(cameraBy, cameraPath);
                    if (camera == null)
                        return TestRunner.Instance.errorCameraNotFound;
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(foundGameObject, camera));

            }
            return response;

        }
    }
}
