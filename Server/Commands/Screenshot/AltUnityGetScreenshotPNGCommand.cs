using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustUnityTester.Server;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetScreenshotPNGCommand : AltUnityCommand {
        AltClientSocketHandler handler;

        public AltUnityGetScreenshotPNGCommand(AltClientSocketHandler handler) {
            this.handler = handler;
        }

        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("getScreenshotPNG");
            AltUnityRunner._altUnityRunner.StartCoroutine(AltUnityRunner._altUnityRunner.TakeScreenshot(handler));
            return "Ok";
        }
    }
}
