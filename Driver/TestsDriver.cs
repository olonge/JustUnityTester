using System.Collections.Generic;
using JustUnityTester.Core;
using JustUnityTester.Driver.Commands;
using JustUnityTester.Driver.Primitives;

namespace JustUnityTester {
    public class TestsDriver {
        public System.Net.Sockets.TcpClient Socket;
        public SocketSettings socketSettings;
        public static readonly string VERSION = "1.5.6";
        public static string requestSeparatorString;
        public static string requestEndingString;

        public TestsDriver(string tcp_ip = "127.0.0.1", int tcp_port = 13000, string requestSeparator = ";", string requestEnding = "&", bool logFlag = false) {

            int timeout = 10;
            int retryPeriod = 5;
            while (timeout > 0) {
                try {
                    Socket = new System.Net.Sockets.TcpClient();
                    Socket.Connect(tcp_ip, tcp_port);
                    Socket.SendTimeout = 5000;
                    Socket.ReceiveTimeout = 5000;

                    socketSettings = new SocketSettings(Socket, requestSeparator, requestEnding, logFlag);
                    CheckServerVersion();
                    EnableLogging();
                    break;
                } catch (System.Exception e) {
                    if (Socket != null)
                        Stop();
                    string errorMessage = "Trying to reach AltUnity Server at port" + tcp_port + ",retrying in " + retryPeriod + " (timing out in " + timeout + " secs)...";
                    System.Console.WriteLine(errorMessage);
                    timeout -= retryPeriod;
                    System.Threading.Thread.Sleep(retryPeriod * 1000);
#if UNITY_EDITOR
                    UnityEngine.Debug.Log(errorMessage);
#endif
                }
                if (timeout <= 0) {
                    throw new System.Exception("Could not create connection to " + tcp_ip + ":" + tcp_port);
                }
            }

        }
        public string CheckServerVersion() => new CheckServerVersion(socketSettings).Execute();
        private void EnableLogging() => new EnableLogging(socketSettings).Execute();

        public void Stop() => new StopCommand(socketSettings).Execute();
        public void LoadScene(string scene, bool loadSingle = true) {
            new AltUnityLoadScene(socketSettings, scene, loadSingle).Execute();
        }
        public List<string> GetAllLoadedScenes() => new AltUnityGetAllLoadedScenes(socketSettings).Execute();

        public List<TestObject> FindObjects(By by, string value, By cameraBy = By.NAME, string cameraPath = "", bool enabled = true) {
            return new AltUnityFindObjects(socketSettings, by, value, cameraBy, cameraPath, enabled).Execute();
        }
        public List<TestObject> FindObjectsWhichContain(By by, string value, By cameraBy = By.NAME, string cameraPath = "", bool enabled = true) {
            return new AltUnityFindObjectsWhichContain(socketSettings, by, value, cameraBy, cameraPath, enabled).Execute();
        }
        [System.Obsolete()]
        public TestObject FindObject(By by, string value, string cameraName, bool enabled = true) {
            return new AltUnityFindObject(socketSettings, by, value, By.NAME, cameraName, enabled).Execute();
        }
        public TestObject FindObject(By by, string value, By cameraBy = By.NAME, string cameraValue = "", bool enabled = true) {
            return new AltUnityFindObject(socketSettings, by, value, cameraBy, cameraValue, enabled).Execute();
        }
        public TestObject FindObjectWhichContains(By by, string value, By cameraBy = By.NAME, string cameraValue = "", bool enabled = true) {
            return new AltUnityFindObjectWhichContains(socketSettings, by, value, cameraBy, cameraValue, enabled).Execute();
        }

        public void SetTimeScale(float timeScale) {
            new AltUnitySetTimeScale(socketSettings, timeScale).Execute();
        }
        public float GetTimeScale() {
            return new AltUnityGetTimeScale(socketSettings).Execute();
        }
        public string CallStaticMethods(string typeName, string methodName,
            string parameters, string typeOfParameters = "", string assemblyName = "") {
            return new AltUnityCallStaticMethods(socketSettings, typeName, methodName, parameters, typeName, assemblyName).Execute();
        }
        public void DeletePlayerPref() {
            new AltUnityDeletePlayerPref(socketSettings).Execute();
        }
        public void DeleteKeyPlayerPref(string keyName) {
            new AltUnityDeleteKeyPlayerPref(socketSettings, keyName).Execute();
        }
        public void SetKeyPlayerPref(string keyName, int valueName) {
            new AltUnitySetKeyPLayerPref(socketSettings, keyName, valueName).Execute();
        }
        public void SetKeyPlayerPref(string keyName, float valueName) {
            new AltUnitySetKeyPLayerPref(socketSettings, keyName, valueName).Execute();
        }
        public void SetKeyPlayerPref(string keyName, string valueName) {
            new AltUnitySetKeyPLayerPref(socketSettings, keyName, valueName).Execute();
        }
        public int GetIntKeyPlayerPref(string keyName) {
            return new AltUnityGetIntKeyPLayerPref(socketSettings, keyName).Execute();
        }
        public float GetFloatKeyPlayerPref(string keyName) {
            return new AltUnityGetFloatKeyPlayerPref(socketSettings, keyName).Execute();
        }
        public string GetStringKeyPlayerPref(string keyName) {
            return new AltUnityGetStringKeyPlayerPref(socketSettings, keyName).Execute();
        }
        public string GetCurrentScene() {
            return new AltUnityGetCurrentScene(socketSettings).Execute();
        }
        public void Swipe(AltUnityVector2 start, AltUnityVector2 end, float duration) {
            new AltUnitySwipe(socketSettings, start, end, duration).Execute();
        }
        public void SwipeAndWait(AltUnityVector2 start, AltUnityVector2 end, float duration) {
            new AltUnitySwipeAndWait(socketSettings, start, end, duration).Execute();
        }
        public void MultipointSwipe(AltUnityVector2[] positions, float duration) {
            new AltUnityMultipointSwipe(socketSettings, positions, duration).Execute();
        }
        public void MultipointSwipeAndWait(AltUnityVector2[] positions, float duration) {
            new AltUnityMultipointSwipeAndWait(socketSettings, positions, duration).Execute();
        }
        public void HoldButton(AltUnityVector2 position, float duration) {
            Swipe(position, position, duration);
        }
        public void HoldButtonAndWait(AltUnityVector2 position, float duration) {
            SwipeAndWait(position, position, duration);
        }
        public void PressKey(AltUnityKeyCode keyCode, float power = 1, float duration = 1) {
            new AltUnityPressKey(socketSettings, keyCode, power, duration).Execute();
        }
        public void PressKeyAndWait(AltUnityKeyCode keyCode, float power = 1, float duration = 1) {
            new AltUnityPressKeyAndWait(socketSettings, keyCode, power, duration).Execute();
        }
        public void MoveMouse(AltUnityVector2 location, float duration = 0) {
            new AltUnityMoveMouse(socketSettings, location, duration).Execute();
        }

        public void MoveMouseAndWait(AltUnityVector2 location, float duration = 0) {
            new AltUnityMoveMouseAndWait(socketSettings, location, duration).Execute();
        }

        public void ScrollMouse(float speed, float duration = 0) {
            new AltUnityScrollMouse(socketSettings, speed, duration).Execute();
        }
        public void ScrollMouseAndWait(float speed, float duration = 0) {
            new AltUnityScrollMouseAndWait(socketSettings, speed, duration).Execute();
        }
        public TestObject TapScreen(float x, float y) {
            return new AltUnityTapScreen(socketSettings, x, y).Execute();
        }
        public void TapCustom(float x, float y, int count, float interval = 0.1f) {
            new AltUnityTapCustom(socketSettings, x, y, count, interval).Execute();
        }
        public void Tilt(AltUnityVector3 acceleration, float duration = 0) {
            new AltUnityTilt(socketSettings, acceleration, duration).Execute();
        }
        public void TiltAndWait(AltUnityVector3 acceleration, float duration = 0) {
            new AltUnityTiltAndWait(socketSettings, acceleration, duration).Execute();
        }
        public List<TestObject> GetAllElements(By cameraBy = By.NAME, string cameraPath = "", bool enabled = true) {
            return new AltUnityGetAllElements(socketSettings, cameraBy, cameraPath, enabled).Execute();
        }
        public string WaitForCurrentSceneToBe(string sceneName, double timeout = 10, double interval = 1) {
            return new AltUnityWaitForCurrentSceneToBe(socketSettings, sceneName, timeout, interval).Execute();
        }

        public TestObject WaitForObject(By by, string value, By cameraBy = By.NAME, string cameraPath = "", bool enabled = true, double timeout = 20, double interval = 0.5) {
            return new AltUnityWaitForObject(socketSettings, by, value, cameraBy, cameraPath, enabled, timeout, interval).Execute();
        }
        [System.Obsolete()]
        public void WaitForObjectNotBePresent(By by, string value, string cameraName, bool enabled = true, double timeout = 20, double interval = 0.5) {
            new AltUnityWaitForObjectNotBePresent(socketSettings, by, value, By.NAME, cameraName, enabled, timeout, interval).Execute();
        }
        public void WaitForObjectNotBePresent(By by, string value, By cameraBy = By.NAME, string cameraPath = "", bool enabled = true, double timeout = 20, double interval = 0.5) {
            new AltUnityWaitForObjectNotBePresent(socketSettings, by, value, cameraBy, cameraPath, enabled, timeout, interval).Execute();
        }
        [System.Obsolete("Use instead WaitForObjectWhichContains")]
        public TestObject WaitForObjectWhichContains(By by, string value, string cameraName, bool enabled = true, double timeout = 20, double interval = 0.5) {
            return new AltUnityWaitForObjectWhichContains(socketSettings, by, value, By.NAME, cameraName, enabled, timeout, interval).Execute();
        }
        public TestObject WaitForObjectWhichContains(By by, string value, By cameraBy = By.NAME, string cameraPath = "", bool enabled = true, double timeout = 20, double interval = 0.5) {
            return new AltUnityWaitForObjectWhichContains(socketSettings, by, value, cameraBy, cameraPath, enabled, timeout, interval).Execute();
        }

        public TestObject WaitForObjectWithText(By by, string value, string text, By cameraBy = By.NAME, string cameraPath = "", bool enabled = true, double timeout = 20, double interval = 0.5) {
            return new AltUnityWaitForObjectWithText(socketSettings, by, value, text, cameraBy, cameraPath, enabled, timeout, interval).Execute();
        }
        public List<string> GetAllScenes() => new AltUnityGetAllScenes(socketSettings).Execute();
        public List<TestObject> GetAllCameras() => new AltUnityGetAllCameras(socketSettings).Execute();
        public TestTextureInformation GetScreenshot(AltUnityVector2 size = default) => new Screenshot(socketSettings, size).Execute();
        public TestTextureInformation GetScreenshot(int id, AltUnityColor color, float width, AltUnityVector2 size = default) {
            return new Screenshot(socketSettings, id, color, width, size).Execute();
        }
        public TestTextureInformation GetScreenshot(AltUnityVector2 coordinates, AltUnityColor color, float width, out TestObject selectedObject, AltUnityVector2 size = default) {
            return new Screenshot(socketSettings, coordinates, color, width, size).Execute(out selectedObject);
        }
        public void GetPNGScreenshot(string path) => new PNGScreenshot(socketSettings, path).Execute();
    }
}