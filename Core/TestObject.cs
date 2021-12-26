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
            return new GetComponentProperty(socketSettings, componentName, propertyName, assemblyName, this).Execute();
        }
        public string SetComponentProperty(string componentName, string propertyName, string value, string assemblyName = null) {
            return new SetComponentProperty(socketSettings, componentName, propertyName, value, assemblyName, this).Execute();
        }
        public string CallComponentMethod(string componentName, string methodName, string parameters, string typeOfParameters = "", string assemblyName = null) {
            return new CallComponentMethod(socketSettings, componentName, methodName, parameters, typeOfParameters, assemblyName, this).Execute();
        }
        public string GetText() => new GetText(socketSettings, this).Execute();
        public TestObject SetText(string text) => new SetText(socketSettings, this, text).Execute();

        public TestObject ClickEvent() => new ClickEvent(socketSettings, this).Execute();

        public TestObject Tap() => new Tap(socketSettings, this, 1).Execute();
        public TestObject DoubleTap() => new Tap(socketSettings, this, 2).Execute();

        public TestObject PointerUpFromObject() => new PointerUpFromObject(socketSettings, this).Execute();
        public TestObject PointerDownFromObject() => new PointerDownFromObject(socketSettings, this).Execute();
        public TestObject PointerEnterObject() => new PointerEnterObject(socketSettings, this).Execute();
        public TestObject PointerExitObject() => new PointerExitObject(socketSettings, this).Execute();

        public List<TestComponent> GetAllComponents() => new GetAllComponents(socketSettings, this).Execute();
        public List<TestProperty> GetAllProperties(TestComponent testComponent) => new GetAllProperties(socketSettings, testComponent, this).Execute();

        public List<string> GetAllMethods(TestComponent testComponent, TestMethodSelection methodSelection = TestMethodSelection.ALLMETHODS) =>
            new GetAllMethods(socketSettings, testComponent, this, methodSelection).Execute();
    }
}