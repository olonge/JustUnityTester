using System.Collections.Generic;
using JustUnityTester.Driver.Commands;
using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Core {
    public class TestObject {
        public string name;
        public int id;
        public int x;
        public int y;
        public int z;
        public int mobileY;
        public string type;
        public bool enabled;
        public float worldX;
        public float worldY;
        public float worldZ;
        public int idCamera;
        public int parentId;
        public int transformId;
        [Newtonsoft.Json.JsonIgnore]
        public SocketSettings socketSettings;
        public TestObject(string name, int id = 0, int x = 0, int y = 0, int z = 0, int mobileY = 0, string type = "", bool enabled = true, float worldX = 0, float worldY = 0, float worldZ = 0, int idCamera = 0, int parentId = 0, int transformId = 0) {
            this.name = name;
            this.id = id;
            this.x = x;
            this.y = y;
            this.z = z;
            this.mobileY = mobileY;
            this.type = type;
            this.enabled = enabled;
            this.worldX = worldX;
            this.worldY = worldY;
            this.worldZ = worldZ;
            this.idCamera = idCamera;
            this.parentId = parentId;
            this.transformId = transformId;
        }
        public AltUnityVector2 getScreenPosition() {
            return new AltUnityVector2(x, y);
        }
        public AltUnityVector3 getWorldPosition() {
            return new AltUnityVector3(worldX, worldY, worldZ);
        }
        public string GetComponentProperty(string componentName, string propertyName, string assemblyName = null) {
            return new AltUnityGetComponentProperty(socketSettings, componentName, propertyName, assemblyName, this).Execute();
        }
        public string SetComponentProperty(string componentName, string propertyName, string value, string assemblyName = null) {
            return new AltUnitySetComponentProperty(socketSettings, componentName, propertyName, value, assemblyName, this).Execute();
        }
        public string CallComponentMethod(string componentName, string methodName, string parameters, string typeOfParameters = "", string assemblyName = null) {
            return new AltUnityCallComponentMethod(socketSettings, componentName, methodName, parameters, typeOfParameters, assemblyName, this).Execute();
        }
        public string GetText() => new AltUnityGetText(socketSettings, this).Execute();
        public TestObject SetText(string text) => new AltUnitySetText(socketSettings, this, text).Execute();
        public TestObject ClickEvent() => new AltUnityClickEvent(socketSettings, this).Execute();
        public TestObject PointerUpFromObject() => new AltUnityPointerUpFromObject(socketSettings, this).Execute();
        public TestObject PointerDownFromObject() => new AltUnityPointerDownFromObject(socketSettings, this).Execute();
        public TestObject PointerEnterObject() => new AltUnityPointerEnterObject(socketSettings, this).Execute();
        public TestObject PointerExitObject() => new AltUnityPointerExitObject(socketSettings, this).Execute();
        public TestObject Tap() => new AltUnityTap(socketSettings, this, 1).Execute();
        public TestObject DoubleTap() => new AltUnityTap(socketSettings, this, 2).Execute();
        public List<TestComponent> GetAllComponents() => new AltUnityGetAllComponents(socketSettings, this).Execute();
        public List<AltUnityProperty> GetAllProperties(TestComponent altUnityComponent) =>
            new AltUnityGetAllProperties(socketSettings, altUnityComponent, this).Execute();
        public List<string> GetAllMethods(TestComponent testComponent, AltUnityMethodSelection methodSelection = AltUnityMethodSelection.ALLMETHODS) =>
            new AltUnityGetAllMethods(socketSettings, testComponent, this, methodSelection).Execute();
    }
}