using System.Collections.Generic;
using System.IO;
using JustUnityTester.Core;

namespace JustUnityTester.Editor {
    public class EditorSetup {
        public static string PreviousScenePath;
        public static UnityEngine.SceneManagement.Scene SceneWithAltUnityRunner;
        public static string SceneWithAltUnityRunnerPath;
        public static UnityEngine.Object AltUnityRunner;
        public static UnityEngine.SceneManagement.Scene copyScene;

        public static string PrefabName = "TestsRunnerPrefab";


        public static void CreateJsonFileForInputMappingOfAxis() {
            string gameDataProjectFilePath = "/Resources/JustUnityTester/JustUnityTesterInputAxisData.json";
            var inputManager = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            UnityEditor.SerializedObject obj = new UnityEditor.SerializedObject(inputManager);

            UnityEditor.SerializedProperty axisArray = obj.FindProperty("m_Axes");

            if (axisArray.arraySize == 0)
                UnityEngine.Debug.Log("No Axes");
            List<TestAxis> axisList = new List<TestAxis>();
            for (int i = 0; i < axisArray.arraySize; ++i) {
                var axis = axisArray.GetArrayElementAtIndex(i);

                var name = axis.FindPropertyRelative("m_Name").stringValue;
                var inputType = (InputType)axis.FindPropertyRelative("type").intValue;
                var negativeButton = axis.FindPropertyRelative("negativeButton").stringValue;
                var positiveButton = axis.FindPropertyRelative("positiveButton").stringValue;
                var altPositiveButton = axis.FindPropertyRelative("altPositiveButton").stringValue;
                var altNegativeButton = axis.FindPropertyRelative("altNegativeButton").stringValue;
                axisList.Add(new TestAxis(name, negativeButton, positiveButton, altPositiveButton, altNegativeButton));
            }

            string dataAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(axisList);
            string filePath = UnityEngine.Application.dataPath + gameDataProjectFilePath;

            if (!UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources"))
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");

            if (!UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources/JustUnityTester"))
                UnityEditor.AssetDatabase.CreateFolder("Assets/Resources", "JustUnityTester");

            File.WriteAllText(filePath, dataAsJson);
        }


        public static void InsertRunnerPrefabIntoScene() {
            var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
            InsertRunnerPrefabIntoScene(activeScene);
        }
        public static void InsertRunnerPrefabIntoScene(string scene, int port = 13000) {
            UnityEngine.Debug.Log("Adding Tests-Runner Prefab into the [" + scene + "] scene.");

            var altUnityRunner = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.GameObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets(PrefabName)[0]));

            SceneWithAltUnityRunner = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scene);
            AltUnityRunner = UnityEditor.PrefabUtility.InstantiatePrefab(altUnityRunner);

            var component = ((UnityEngine.GameObject)AltUnityRunner).GetComponent<global::TestRunner>();
            if (TesterEditor.Config == null) {
                component.ShowInputs = false;
                component.showPopUp = true;
                component.SocketPortNumber = port;
            } else {
                component.ShowInputs = TesterEditor.Config.inputVisualizer;
                component.showPopUp = TesterEditor.Config.showPopUp;
                component.SocketPortNumber = TesterEditor.Config.serverPort;
            }

            //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            //UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
            //UnityEngine.Debug.Log("Scene successfully modified.");
        }

        public enum InputType {
            KeyOrMouseButton,
            MouseMovement,
            JoystickAxis,
        };

    }
}