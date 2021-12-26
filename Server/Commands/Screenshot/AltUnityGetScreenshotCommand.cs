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
            AltUnityRunner._altUnityRunner.LogMessage("getScreenshot" + size);
            AltUnityRunner._altUnityRunner.StartCoroutine(AltUnityRunner._altUnityRunner.TakeTexturedScreenshot(size, handler));
            return "Ok";
        }
    }
}
