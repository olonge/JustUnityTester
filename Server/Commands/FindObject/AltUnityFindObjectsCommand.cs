using System.Linq;
using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class AltUnityFindObjectsCommand : AltUnityBaseClassFindObjectsCommand {
        string stringSent;

        public AltUnityFindObjectsCommand(string stringSent) {
            this.stringSent = stringSent;
        }

        public override string Execute() {
            var pieces = stringSent.Split(new string[] { TestRunner.Instance.requestSeparatorString }, System.StringSplitOptions.None);
            string objectName = pieces[0];
            TestRunner.Instance.LogMessage("findObjects for: " + objectName);
            By cameraBy = (By)System.Enum.Parse(typeof(By), pieces[1]);
            string cameraPath = pieces[2];
            bool enabled = System.Convert.ToBoolean(pieces[3]);
            UnityEngine.Camera camera = null;
            if (!cameraPath.Equals("//")) {
                camera = GetCamera(cameraBy, cameraPath);
                if (camera == null)
                    return TestRunner.Instance.errorCameraNotFound;
            }
            var path = ProcessPath(objectName);
            var isDirectChild = IsNextElementDirectChild(path[0]);
            System.Collections.Generic.List<TestObject> foundObjects = new System.Collections.Generic.List<TestObject>();
            foreach (UnityEngine.GameObject testableObject in FindObjects(null, path, 1, false, isDirectChild, enabled)) {
                foundObjects.Add(TestRunner.Instance.GameObjectToAltUnityObject(testableObject, camera));
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(foundObjects);

        }
    }
}
