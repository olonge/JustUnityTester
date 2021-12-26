using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class Swipe : BaseCommand {
        TestVector2 start;
        TestVector2 end;
        float duration;
        public Swipe(SocketSettings socketSettings, TestVector2 start, TestVector2 end, float duration) : base(socketSettings) {
            this.start = start;
            this.end = end;
            this.duration = duration;
        }
        public void Execute() {
            var vectorStartJson = PositionToJson(start);
            var vectorEndJson = PositionToJson(end);

            Socket.Client.Send(toBytes(CreateCommand("multipointSwipe", vectorStartJson, vectorEndJson, duration.ToString())));
            var data = Recvall();
            if (data.Equals("Ok"))
                return;
            HandleErrors(data);
        }
    }
}