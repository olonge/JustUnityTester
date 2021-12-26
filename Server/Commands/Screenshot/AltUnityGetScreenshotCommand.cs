using JustUnityTester.Server;
using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetScreenshotCommand : AltUnityCommand {
        Vector2 size;
        AltClientSocketHandler handler;

        public AltUnityGetScreenshotCommand(Vector2 size, AltClientSocketHandler handler) {
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
