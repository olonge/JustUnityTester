using System.Collections.Generic;

namespace JustUnityTester.Server {
    public delegate void SendResponse();

    public class ResponseQueue {
        Queue<SendResponse> _responseQueue = new Queue<SendResponse>();
        readonly object _queueLock = new object();

        public void Cycle() {
            lock (_queueLock) {
                if (_responseQueue.Count > 0)
                    _responseQueue.Dequeue()();
            }
        }

        public void ScheduleResponse(SendResponse newResponse) {
            lock (_queueLock) {
                if (_responseQueue.Count < 100)
                    _responseQueue.Enqueue(newResponse);
            }
        }
    }
}
