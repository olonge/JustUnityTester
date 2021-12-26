using UnityEngine;

namespace JustUnityTester.Server.Commands {
    class MoveMouse : AltUnityCommand {
        Vector2 location;
        float duration;

        public MoveMouse(Vector2 location, float duration) {
            this.location = location;
            this.duration = duration;
        }

        public override string Execute() {
#if ALTUNITYTESTER
                AltUnityRunner._altUnityRunner.LogMessage("moveMouse to: " + location);
                Input.MoveMouse(location, duration);
                return "Ok";
#else
            return null; ;
#endif
        }
    }
}
