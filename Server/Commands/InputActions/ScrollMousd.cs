namespace JustUnityTester.Server.Commands {
    class ScrollMousd : AltUnityCommand {
        float scrollValue;
        float duration;

        public ScrollMousd(float scrollValue, float duration) {
            this.scrollValue = scrollValue;
            this.duration = duration;
        }

        public override string Execute() {
#if ALTUNITYTESTER
            AltUnityRunner._altUnityRunner.LogMessage("scrollMouse with: " + scrollValue);
            Input.Scroll(scrollValue, duration);
            return "Ok";
#else
            return null;
#endif
        }
    }
}
