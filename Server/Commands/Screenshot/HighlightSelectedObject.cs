using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class HighlightSelectedObject : Command {
        int id;
        string ColorAndWidth;
        Vector2 size;
        ClientSocket handler;

        public HighlightSelectedObject(int id, string colorAndWidth, Vector2 size, ClientSocket handler) {
            this.id = id;
            ColorAndWidth = colorAndWidth;
            this.size = size;
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("HightlightObject wiht id: " + id);
            var pieces = ColorAndWidth.Split(new[] { "!-!" }, System.StringSplitOptions.None);
            var piecesColor = pieces[0].Split(new[] { "!!" }, System.StringSplitOptions.None);
            float red = float.Parse(piecesColor[0]);
            float green = float.Parse(piecesColor[1]);
            float blue = float.Parse(piecesColor[2]);
            float alpha = float.Parse(piecesColor[3]);

            Color color = new Color(red, green, blue, alpha);
            float width = float.Parse(pieces[1]);
            var gameObject = TestRunner.GetGameObject(id);

            if (gameObject != null) {
                TestRunner.Instance.StartCoroutine(TestRunner.Instance.HighLightSelectedObjectCorutine(gameObject, color, width, size, handler));
            } else
                new GetScreenshot(size, handler).Execute();
            return "Ok";
        }
    }
}
