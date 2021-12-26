using JustUnityTester.Server;

namespace JustUnityTester.Server.Commands {
    class LoadScene : Command {
        string scene;
        UnityEngine.SceneManagement.LoadSceneMode mode;
        AltClientSocketHandler handler;

        public LoadScene(string scene, bool loadSingle, AltClientSocketHandler handler) {
            mode = loadSingle
                ? UnityEngine.SceneManagement.LoadSceneMode.Single
                : UnityEngine.SceneManagement.LoadSceneMode.Additive;

            this.scene = scene;
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("LoadScene " + scene);
            string response = TestRunner.Instance.errorNotFoundMessage;

            var sceneLoadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene, mode);
            sceneLoadingOperation.completed += SceneLoaded;

            response = "Ok";
            return response;
        }

        private void SceneLoaded(UnityEngine.AsyncOperation obj) {
            TestRunner.logMessage = "Scene Loaded";
            handler.SendResponse("Scene Loaded");
        }
    }
}
