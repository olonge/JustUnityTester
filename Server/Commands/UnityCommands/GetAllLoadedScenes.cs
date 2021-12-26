namespace JustUnityTester.Server.Commands {
    class GetAllLoadedScenes : AltUnityCommand {
        private System.Collections.Generic.List<string> sceneNames = new System.Collections.Generic.List<string>();

        public GetAllLoadedScenes() {
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("getAllLoadedScenes");
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++) {

                var sceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name;
                sceneNames.Add(sceneName);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(sceneNames);
        }

    }
}
