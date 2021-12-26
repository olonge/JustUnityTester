using JustUnityTester.Core;

namespace JustUnityTester.Driver.Commands {
    public class WaitForObjectNotBePresent : BaseFindObjects {
        By by;
        string value;
        By cameraBy;
        string cameraPath;
        bool enabled;
        double timeout;
        double interval;
        public WaitForObjectNotBePresent(SocketSettings socketSettings, By by, string value, By cameraBy, string cameraPath, bool enabled, double timeout, double interval) : base(socketSettings) {
            this.by = by;
            this.value = value;
            this.cameraBy = cameraBy;
            this.cameraPath = cameraPath;
            this.enabled = enabled;
            this.timeout = timeout;
            this.interval = interval;
        }
        public void Execute() {
            double time = 0;
            bool found = false;
            string path = SetPath(by, value);
            TestObject altElement;
            while (time <= timeout) {
                found = false;
                try {
                    altElement = new FindObject(SocketSettings, by, value, cameraBy, cameraPath, enabled).Execute();
                    found = true;
                    System.Threading.Thread.Sleep(System.Convert.ToInt32(interval * 1000));
                    time += interval;
                    System.Diagnostics.Debug.WriteLine("Waiting for element " + path + " to not be present");
                } catch (System.Exception) {
                    break;
                }
            }
            if (found)
                throw new Exceptions.WaitTimeOutException("Element " + path + " still found after " + timeout + " seconds");
        }
    }
}