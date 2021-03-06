using System.Linq;

namespace Assets.AltUnityTester.AltUnityServer.Commands
{
    class AltUnityFindObjectWhereNameContainsCommand :AltUnityCommand
    {
        string methodParameters;

        public AltUnityFindObjectWhereNameContainsCommand (string methodParameters)
        {
            this.methodParameters = methodParameters;
        }

        public override string Execute()
        {
            var pieces = methodParameters.Split(new string[] { AltUnityRunner._altUnityRunner.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            AltUnityRunner._altUnityRunner.LogMessage("find object where name contains:" + objectName);
            string cameraName = pieces[1];
            string response = AltUnityRunner._altUnityRunner.errorNotFoundMessage;
            UnityEngine.Camera camera = null;
            if (cameraName != null)
            {
                camera = UnityEngine.Camera.allCameras.ToList().Find(c => c.name.Equals(cameraName));
            }
            foreach (UnityEngine.GameObject testableObject in UnityEngine.GameObject.FindObjectsOfType<UnityEngine.GameObject>())
            {
                if (testableObject.name.Contains(objectName))
                {
                    response = Newtonsoft.Json.JsonConvert.SerializeObject(AltUnityRunner._altUnityRunner.GameObjectToAltUnityObject(testableObject, camera));
                    break;
                }
            }
            return response;
        }
    }
}
