using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    public class GetCurrentScene : Command {
        public override string Execute() {
            TestRunner.Instance.LogMessage("get current scene");
            TestObject scene = new TestObject(name: UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                                                             type: "UnityScene");
            return UnityEngine.JsonUtility.ToJson(scene);

        }
    }
}