using JustUnityTester.Core;

namespace JustUnityTester.Editor {
    public class AltUnityBuilder {
        public static string PreviousScenePath;
        public static UnityEngine.SceneManagement.Scene SceneWithAltUnityRunner;
        public static string SceneWithAltUnityRunnerPath;
        public static UnityEngine.Object AltUnityRunner;
        public static UnityEngine.SceneManagement.Scene copyScene;


        public static void CreateJsonFileForInputMappingOfAxis() {
            string gameDataProjectFilePath = "/Resources/AltUnityTester/AltUnityTesterInputAxisData.json";
            var inputManager = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            UnityEditor.SerializedObject obj = new UnityEditor.SerializedObject(inputManager);

            UnityEditor.SerializedProperty axisArray = obj.FindProperty("m_Axes");

            if (axisArray.arraySize == 0)
                UnityEngine.Debug.Log("No Axes");
            System.Collections.Generic.List<AltUnityAxis> axisList = new System.Collections.Generic.List<AltUnityAxis>();
            for (int i = 0; i < axisArray.arraySize; ++i) {
                var axis = axisArray.GetArrayElementAtIndex(i);

                var name = axis.FindPropertyRelative("m_Name").stringValue;
                var inputType = (InputType)axis.FindPropertyRelative("type").intValue;
                var negativeButton = axis.FindPropertyRelative("negativeButton").stringValue;
                var positiveButton = axis.FindPropertyRelative("positiveButton").stringValue;
                var altPositiveButton = axis.FindPropertyRelative("altPositiveButton").stringValue;
                var altNegativeButton = axis.FindPropertyRelative("altNegativeButton").stringValue;
                axisList.Add(new AltUnityAxis(name, negativeButton, positiveButton, altPositiveButton, altNegativeButton));
            }

            string dataAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(axisList);
            string filePath = UnityEngine.Application.dataPath + gameDataProjectFilePath;
            if (!UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources/AltUnityTester")) {
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");
                UnityEditor.AssetDatabase.CreateFolder("Assets/Resources", "AltUnityTester");
            }
            System.IO.File.WriteAllText(filePath, dataAsJson);
        }

        public static void InsertAltUnityInScene(string scene, int port = 13000) {
            UnityEngine.Debug.Log("Adding AltUnityRunnerPrefab into the [" + scene + "] scene.");
            var altUnityRunner = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.GameObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("AltUnityRunnerPrefab")[0]));

            SceneWithAltUnityRunner = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scene);
            AltUnityRunner = UnityEditor.PrefabUtility.InstantiatePrefab(altUnityRunner);
            var component = ((UnityEngine.GameObject)AltUnityRunner).GetComponent<AltUnityRunner>();
            if (AltUnityTesterEditor.EditorConfiguration == null) {
                component.ShowInputs = false;
                component.showPopUp = true;
                component.SocketPortNumber = port;
            } else {
                component.ShowInputs = AltUnityTesterEditor.EditorConfiguration.inputVisualizer;
                component.showPopUp = AltUnityTesterEditor.EditorConfiguration.showPopUp;
                component.SocketPortNumber = AltUnityTesterEditor.EditorConfiguration.serverPort;
            }

            //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
            UnityEngine.Debug.Log("Scene successfully modified.");
        }
        public static void InsertAltUnityInTheActiveScene() {
            var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
            InsertAltUnityInScene(activeScene);
        }

        public enum InputType {
            KeyOrMouseButton,
            MouseMovement,
            JoystickAxis,
        };

    }
}