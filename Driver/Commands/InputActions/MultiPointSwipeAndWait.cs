using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class MultiPointSwipeAndWait : BaseCommand {
        TestVector2[] positions;
        float duration;

        public MultiPointSwipeAndWait(SocketSettings socketSettings, TestVector2[] positions, float duration) : base(socketSettings) {
            this.positions = positions;
            this.duration = duration;
        }

        public void Execute() {
            new MultiPointSwipe(SocketSettings, positions, duration).Execute();
            System.Threading.Thread.Sleep((int)duration * 1000);
            string data;
            do {
                Socket.Client.Send(toBytes(CreateCommand("actionFinished")));
                data = Recvall();
            } while (data == "No");
            if (data.Equals("Yes"))
                return;
            HandleErrors(data);
        }
    }
}