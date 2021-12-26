using JustUnityTester.Core;

namespace JustUnityTester.Server.Commands {
    class ClickEvent : AltUnityCommand {
        readonly TestObject testObject;

        public ClickEvent(TestObject testObject) => this.testObject = testObject;

        public override string Execute() {
            TestRunner.Instance.LogMessage("ClickEvent on " + testObject);
            TestRunner.Instance.ShowClick(new UnityEngine.Vector2(testObject.getScreenPosition().x, testObject.getScreenPosition().y));

            string response = TestRunner.Instance.errorNotFoundMessage;
            UnityEngine.GameObject foundGameObject = TestRunner.GetGameObject(testObject);

            var pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            UnityEngine.EventSystems.ExecuteEvents.Execute(foundGameObject, pointerEventData, UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);

            response = Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(foundGameObject));
            return response;
        }
    }
}
