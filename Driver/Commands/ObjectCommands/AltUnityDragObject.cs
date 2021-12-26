using JustUnityTester.Core;
using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityDragObject : AltUnityCommandReturningAltElement {
        AltUnityVector2 position;
        TestObject altUnityObject;
        public AltUnityDragObject(SocketSettings socketSettings, AltUnityVector2 position, TestObject altUnityObject) : base(socketSettings) {
            this.position = position;
            this.altUnityObject = altUnityObject;
        }
        public TestObject Execute() {
            var positionJson = PositionToJson(position);
            var altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);

            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("dragObject", positionJson, altObject)));
            return ReceiveAltUnityObject();
        }
    }
}