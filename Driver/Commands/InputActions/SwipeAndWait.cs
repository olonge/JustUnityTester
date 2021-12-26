using JustUnityTester.Driver.Primitives;

namespace JustUnityTester.Driver.Commands {
    public class SwipeAndWait : BaseCommand {
        TestVector2 start;
        TestVector2 end;
        float duration;
        public SwipeAndWait(SocketSettings socketSettings, TestVector2 start, TestVector2 end, float duration) : base(socketSettings) {
            this.start = start;
            this.end = end;
            this.duration = duration;
        }
        public void Execute() {
            new Swipe(SocketSettings, start, end, duration).Execute();
            System.Threading.Thread.Sleep((int)(duration * 1000));
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