using JustUnityTester.Server;

namespace JustUnityTester.Server.Commands {
    class AltUnityLoadSceneCommand : AltUnityCommand {
        string scene;
        UnityEngine.SceneManagement.LoadSceneMode mode;
        AltClientSocketHandler handler;

        public AltUnityLoadSceneCommand(string scene, bool loadSingle, AltClientSocketHandler handler) {
            mode = loadSingle
                ? UnityEngine.SceneManagement.LoadSceneMode.Single
                : UnityEngine.SceneManagement.LoadSceneMode.Additive;

            this.scene = scene;
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("LoadScene " + scene);
            string response = TestRunner._altUnityRunner.errorNotFoundMessage;

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
