namespace JustUnityTester.Server.Commands {
    public class AltUnityGetAllScenesCommand : AltUnityCommand {
        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("getAllScenes");
            System.Collections.Generic.List<string> SceneNames = new System.Collections.Generic.List<string>();
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++) {
                var s = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
                SceneNames.Add(s);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(SceneNames);
        }
    }
}