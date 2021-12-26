using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class AltUnityMultipointSwipe : BaseCommand {
        AltUnityVector2[] positions;
        float duration;

        public AltUnityMultipointSwipe(SocketSettings socketSettings, AltUnityVector2[] positions, float duration) : base(socketSettings) {
            this.positions = positions;
            this.duration = duration;
        }

        public void Execute() {
            var args = new System.Collections.Generic.List<string> { "multipointSwipeChain", duration.ToString() };
            foreach (var pos in positions) {
                var posJson = PositionToJson(pos);
                args.Add(posJson);
            }

            Socket.Client.Send(toBytes(CreateCommand(args.ToArray())));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);
        }
    }
}