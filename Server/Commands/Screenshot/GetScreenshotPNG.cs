using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustUnityTester.Server;

namespace JustUnityTester.Server.Commands {
    class GetScreenshotPNG : Command {
        AltClientSocketHandler handler;

        public GetScreenshotPNG(AltClientSocketHandler handler) {
            this.handler = handler;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("getScreenshotPNG");
            TestRunner.Instance.StartCoroutine(TestRunner.Instance.TakeScreenshot(handler));
            return "Ok";
        }
    }
}
