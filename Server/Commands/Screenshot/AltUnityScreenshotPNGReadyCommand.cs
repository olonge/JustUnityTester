using System.Text;

namespace JustUnityTester.Server.Commands {
    class AltUnityScreenshotPNGReadyCommand : AltUnityCommand {
        byte[] screenshotData;

        public AltUnityScreenshotPNGReadyCommand(byte[] screenshotData) {
            this.screenshotData = screenshotData;
        }

        public override string Execute() {
            return Encoding.ASCII.GetString(screenshotData);
        }
    }
}