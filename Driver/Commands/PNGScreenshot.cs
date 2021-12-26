using System.IO;

namespace JustUnityTester.Driver.Commands {
    public class PNGScreenshot : BaseCommand {
        string path;
        public PNGScreenshot(SocketSettings socketSettings, string path) : base(socketSettings) {
            this.path = path;
        }

        public void Execute() {
            Socket.Client.Send(toBytes(CreateCommand("getPNGScreenshot")));

            var message = Recvall();
            string screenshotData = "";
            if (message == "Ok")
                screenshotData = Recvall();

            File.WriteAllBytes(path, System.Convert.FromBase64String(screenshotData));
        }
    }
}