using JustUnityTester.Core;
using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class AltUnityHightlightObjectFromCoordinatesCommand : AltUnityCommand {
        Vector2 screenCoordinates;
        string ColorAndWidth;
        Vector2 size;
        AltClientSocketHandler handler;


        public AltUnityHightlightObjectFromCoordinatesCommand(Vector2 screenCoordinates, string colorAndWidth, Vector2 size, AltClientSocketHandler handler) {
            this.screenCoordinates = screenCoordinates;
            ColorAndWidth = colorAndWidth;
            this.size = size;
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("HightlightObject with coordinates: " + screenCoordinates);
            var pieces = ColorAndWidth.Split(new[] { "!-!" }, System.StringSplitOptions.None);
            var piecesColor = pieces[0].Split(new[] { "!!" }, System.StringSplitOptions.None);
            float red = float.Parse(piecesColor[0]);
            float green = float.Parse(piecesColor[1]);
            float blue = float.Parse(piecesColor[2]);
            float alpha = float.Parse(piecesColor[3]);

            Color color = new Color(red, green, blue, alpha);
            float width = float.Parse(pieces[1]);

            Ray ray = Camera.main.ScreenPointToRay(screenCoordinates);
            RaycastHit[] hits;
            var raycasters = Object.FindObjectsOfType<UnityEngine.UI.GraphicRaycaster>();
            UnityEngine.EventSystems.PointerEventData pointerEventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            pointerEventData.position = screenCoordinates;
            foreach (var raycaster in raycasters) {
                System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> hitUI = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
                raycaster.Raycast(pointerEventData, hitUI);
                foreach (var hit in hitUI) {
                    handler.SendResponse(Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(hit.gameObject)));
                    TestRunner._altUnityRunner.StartCoroutine(TestRunner._altUnityRunner.HighLightSelectedObjectCorutine(hit.gameObject, color, width, size, handler));
                    return "Ok";
                }
            }
            hits = Physics.RaycastAll(ray);
            if (hits.Length > 0) {
                handler.SendResponse(Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner._altUnityRunner.GameObjectToAltUnityObject(hits[hits.Length - 1].transform.gameObject)));
                TestRunner._altUnityRunner.StartCoroutine(TestRunner._altUnityRunner.HighLightSelectedObjectCorutine(hits[hits.Length - 1].transform.gameObject, color, width, size, handler));
            } else {
                handler.SendResponse(Newtonsoft.Json.JsonConvert.SerializeObject(new TestObject("Null")));
                new AltUnityGetScreenshotCommand(size, handler).Execute();
            }
            return "Ok";
        }
    }
}
