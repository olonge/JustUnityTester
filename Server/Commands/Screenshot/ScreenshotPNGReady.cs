using System.Text;

namespace JustUnityTester.Server.Commands {
    class ScreenshotPNGReady : Command {
        byte[] screenshotData;

        public ScreenshotPNGReady(byte[] screenshotData) {
            this.screenshotData = screenshotData;
        }

        public override string Execute() {
            return Encoding.ASCII.GetString(screenshotData);
        }
    }
}