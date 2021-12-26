using JustUnityTester.Server;
using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class GetScreenshot : Command {
        Vector2 size;
        ClientSocket handler;

        public GetScreenshot(Vector2 size, ClientSocket handler) {
            this.size = size;
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("getScreenshot" + size);
            TestRunner.Instance.StartCoroutine(TestRunner.Instance.TakeTexturedScreenshot(size, handler));
            return "Ok";
        }
    }
}
