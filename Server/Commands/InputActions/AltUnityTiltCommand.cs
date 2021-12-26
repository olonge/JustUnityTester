namespace JustUnityTester.Server.Commands {
    class AltUnityTiltCommand : AltUnityCommand {
        UnityEngine.Vector3 acceleration;
        float duration;

        public AltUnityTiltCommand(UnityEngine.Vector3 acceleration, float duration) {
            this.acceleration = acceleration;
            this.duration = duration;
        }

        public override string Execute() {
#if ALTUNITYTESTER
            AltUnityRunner._altUnityRunner.LogMessage("Tilt device with: " + acceleration);
            Input.Acceleration(acceleration, duration);
            return "OK";
#else
            return null;
#endif
        }
    }
}
