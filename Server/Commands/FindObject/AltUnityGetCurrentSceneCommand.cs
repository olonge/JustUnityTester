using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    public class AltUnityGetCurrentSceneCommand : AltUnityCommand {
        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("get current scene");
            TestObject scene = new TestObject(name: UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                                                             type: "UnityScene");
            return UnityEngine.JsonUtility.ToJson(scene);

        }
    }
}