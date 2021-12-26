using JustUnityTester.Core;
using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityGetScreenshot : ReturnedElement {
        int id;
        AltUnityColor color;
        float width;
        AltUnityVector2 size;
        AltUnityVector2 coordinates;

        int option = 0;

        public AltUnityGetScreenshot(SocketSettings socketSettings, AltUnityVector2 size) : base(socketSettings) {
            this.size = size;
            option = 1;
        }
        public AltUnityGetScreenshot(SocketSettings socketSettings, int id, AltUnityColor color, float width, AltUnityVector2 size) : base(socketSettings) {
            this.size = size;
            this.color = color;
            this.width = width;
            this.id = id;
            option = 2;
        }
        public AltUnityGetScreenshot(SocketSettings socketSettings, AltUnityVector2 coordinates, AltUnityColor color, float width, AltUnityVector2 size) : base(socketSettings) {
            this.coordinates = coordinates;
            this.color = color;
            this.width = width;
            this.size = size;
            option = 3;
        }
        public TestTextureInformation Execute(out TestObject selectedObject) {
            selectedObject = null;
            switch (option) {
                case 2:
                    return GetHighlightObjectScreenshot();
                case 3:
                    return GetHighlightObjectFromCoordinatesScreenshot(out selectedObject);
                default:
                    return GetSimpleScreenshot();
            }
        }
        public TestTextureInformation Execute() {
            TestObject selectedObject = null;
            switch (option) {
                case 2:
                    return GetHighlightObjectScreenshot();
                case 3:
                    return GetHighlightObjectFromCoordinatesScreenshot(out selectedObject);
                default:
                    return GetSimpleScreenshot();
            }
        }

        private TestTextureInformation GetSimpleScreenshot() {
            var sizeSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(size);
            Socket.Client.Send(toBytes(CreateCommand("getScreenshot", sizeSerialized)));
            return ReceiveImage();
        }
        private TestTextureInformation GetHighlightObjectScreenshot() {
            var sizeSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(size);
            var colorAndWidth = color.r + "!!" + color.g + "!!" + color.b + "!!" + color.a + "!-!" + width;
            Socket.Client.Send(toBytes(CreateCommand("hightlightObjectScreenshot", id.ToString(), colorAndWidth, sizeSerialized)));
            return ReceiveImage();
        }
        private TestTextureInformation GetHighlightObjectFromCoordinatesScreenshot(out TestObject selectedObject) {
            var coordinatesSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
            var sizeSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(size);
            var colorAndWidth = color.r + "!!" + color.g + "!!" + color.b + "!!" + color.a + "!-!" + width;
            Socket.Client.Send(toBytes(CreateCommand("hightlightObjectFromCoordinatesScreenshot", coordinatesSerialized, colorAndWidth, sizeSerialized)));
            selectedObject = ReceiveAltUnityObject();
            if (selectedObject.name.Equals("Null") && selectedObject.id == 0) {
                selectedObject = null;
            }
            return ReceiveImage();
        }
    }
}