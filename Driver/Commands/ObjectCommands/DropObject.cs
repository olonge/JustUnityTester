using JustUnityTester.Core;
using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class DropObject : ReturnedElement {
        AltUnityVector2 position;
        TestObject altUnityObject;
        public DropObject(SocketSettings socketSettings, AltUnityVector2 position, TestObject altUnityObject) : base(socketSettings) {
            this.position = position;
            this.altUnityObject = altUnityObject;
        }
        public TestObject Execute() {
            var altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
            var positionString = PositionToJson(position);

            Socket.Client.Send(System.Text.Encoding.ASCII.GetBytes(CreateCommand("dropObject", positionString, altObject)));
            return ReceiveAltUnityObject();
        }
    }
}