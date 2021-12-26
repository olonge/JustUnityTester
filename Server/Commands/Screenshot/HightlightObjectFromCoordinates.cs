using JustUnityTester.Core;
using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class HightlightObjectFromCoordinates : Command {
        Vector2 screenCoordinates;
        string ColorAndWidth;
        Vector2 size;
        ClientSocket handler;


        public HightlightObjectFromCoordinates(Vector2 screenCoordinates, string colorAndWidth, Vector2 size, ClientSocket handler) {
            this.screenCoordinates = screenCoordinates;
            ColorAndWidth = colorAndWidth;
            this.size = size;
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("HightlightObject with coordinates: " + screenCoordinates);
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
                    handler.SendResponse(Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(hit.gameObject)));
                    TestRunner.Instance.StartCoroutine(TestRunner.Instance.HighLightSelectedObjectCorutine(hit.gameObject, color, width, size, handler));
                    return "Ok";
                }
            }
            hits = Physics.RaycastAll(ray);
            if (hits.Length > 0) {
                handler.SendResponse(Newtonsoft.Json.JsonConvert.SerializeObject(TestRunner.Instance.GameObjectToAltUnityObject(hits[hits.Length - 1].transform.gameObject)));
                TestRunner.Instance.StartCoroutine(TestRunner.Instance.HighLightSelectedObjectCorutine(hits[hits.Length - 1].transform.gameObject, color, width, size, handler));
            } else {
                handler.SendResponse(Newtonsoft.Json.JsonConvert.SerializeObject(new TestObject("Null")));
                new GetScreenshot(size, handler).Execute();
            }
            return "Ok";
        }
    }
}
