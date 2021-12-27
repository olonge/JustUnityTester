namespace JustUnityTester.Server.Commands {
    public class ClickOnScreen : Command {
        UnityEngine.Vector2 position;
        string count;
        string interval;

        public ClickOnScreen(UnityEngine.Vector2 position, string count, string interval) {
            this.position = position;
            this.count = count;
            this.interval = interval;
        }

        public override string Execute() {
#if ALTUNITYTESTER
            AltUnityRunner._altUnityRunner.LogMessage("Custom click at: " + position);
            var response = AltUnityRunner._altUnityRunner.errorNotFoundMessage;
            
            var pCount = 1;
            var pInterval = 0f;
            int.TryParse(count, out pCount);
            float.TryParse(interval, out pInterval);
            
            Input.SetCustomClick(position, pCount, pInterval);
            response = "Ok";
            return response;
#else
            return null;
#endif
        }
    }
}