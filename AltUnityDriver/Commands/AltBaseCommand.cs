using JustUnityTester.Core;
using JustUnityTester.Driver.Primitives;
using System.Linq;

namespace JustUnityTester.Driver.Commands {
    public class AltBaseCommand {
        private static int BUFFER_SIZE = 1024;
        protected SocketSettings SocketSettings;
        protected System.Net.Sockets.TcpClient Socket;
        public AltBaseCommand(SocketSettings socketSettings) {
            SocketSettings = socketSettings;
            Socket = SocketSettings.socket;
        }
        public string Recvall() {
            string data = "";
            string previousPart = "";
            System.Collections.Generic.List<byte[]> byteArray = new System.Collections.Generic.List<byte[]>();
            int received = 0;
            int receivedZeroBytesCounter = 0;
            int receivedZeroBytesCounterLimit = 2;
            do {
                var bytesReceived = new byte[BUFFER_SIZE];
                received = SocketSettings.socket.Client.Receive(bytesReceived);
                if (received == 0) {
                    if (receivedZeroBytesCounter < receivedZeroBytesCounterLimit) {
                        receivedZeroBytesCounter++;
                        continue;
                    } else {
                        throw new System.Exception("Socket not connected yet");
                    }
                }
                var newBytesReceived = new byte[received];
                for (int i = 0; i < received; i++) {
                    newBytesReceived[i] = bytesReceived[i];
                }
                byteArray.Add(newBytesReceived);
                string part = fromBytes(newBytesReceived);
                string partToSeeAltEnd = previousPart + part;
                if (partToSeeAltEnd.Contains("::altend"))
                    break;
                previousPart = part;

            } while (true);
            data = fromBytes(byteArray.SelectMany(x => x).ToArray());

            try {
                string[] start = new string[] { "altstart::" };
                string[] end = new string[] { "::altend" };
                string[] startLogMessage = new string[] { "::altLog::" };
                data = data.Split(start, System.StringSplitOptions.None)[1].Split(end, System.StringSplitOptions.None)[0];
                var splittedString = data.Split(startLogMessage, System.StringSplitOptions.None);
                var response = splittedString[0];
                data = response;
                var logMessage = splittedString[1];
                if (SocketSettings.logFlag) {
                    WriteInLogFile(logMessage);
                    WriteInLogFile(System.DateTime.Now + ": response received: " + response);
                }

            } catch (System.Exception) {
                System.Diagnostics.Debug.WriteLine("Data received from socket doesn't have correct start and end control strings");
            }

            return data;
        }
        protected void WriteInLogFile(string logMessage) {
            var FileWriter = new System.IO.StreamWriter(@"AltUnityTesterLog.txt", true);
            FileWriter.WriteLine(logMessage);
            FileWriter.Close();
        }

        public string CreateCommand(params string[] arguments) {
            string command = "";
            foreach (var argument in arguments) {
                command += argument + SocketSettings.requestSeparator;
            }
            command += SocketSettings.requestEnding;
            return command;
        }
        protected byte[] toBytes(string text) {
            return System.Text.Encoding.UTF8.GetBytes(text);
        }
        protected string fromBytes(byte[] text) {
            return System.Text.Encoding.UTF8.GetString(text);
        }
        protected string PositionToJson(AltUnityVector2 position) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(position, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }
        protected string PositionToJson(float x, float y) {
            return PositionToJson(new AltUnityVector2(x, y));
        }

        public static void HandleErrors(string data) {
            var typeOfException = data.Split(';')[0];
            switch (typeOfException) {
                case "error:notFound":
                    throw new Assets.AltUnityTester.AltUnityDriver.NotFoundException(data);
                case "error:propertyNotFound":
                    throw new Assets.AltUnityTester.AltUnityDriver.PropertyNotFoundException(data);
                case "error:methodNotFound":
                    throw new Assets.AltUnityTester.AltUnityDriver.MethodNotFoundException(data);
                case "error:componentNotFound":
                    throw new Assets.AltUnityTester.AltUnityDriver.ComponentNotFoundException(data);
                case "error:couldNotPerformOperation":
                    throw new Assets.AltUnityTester.AltUnityDriver.CouldNotPerformOperationException(data);
                case "error:couldNotParseJsonString":
                    throw new Assets.AltUnityTester.AltUnityDriver.CouldNotParseJsonStringException(data);
                case "error:incorrectNumberOfParameters":
                    throw new Assets.AltUnityTester.AltUnityDriver.IncorrectNumberOfParametersException(data);
                case "error:failedToParseMethodArguments":
                    throw new Assets.AltUnityTester.AltUnityDriver.FailedToParseArgumentsException(data);
                case "error:objectNotFound":
                    throw new Assets.AltUnityTester.AltUnityDriver.ObjectWasNotFoundException(data);
                case "error:propertyCannotBeSet":
                    throw new Assets.AltUnityTester.AltUnityDriver.PropertyNotFoundException(data);
                case "error:nullReferenceException":
                    throw new Assets.AltUnityTester.AltUnityDriver.NullReferenceException(data);
                case "error:unknownError":
                    throw new Assets.AltUnityTester.AltUnityDriver.UnknownErrorException(data);
                case "error:formatException":
                    throw new Assets.AltUnityTester.AltUnityDriver.FormatException(data);
            }
        }
        public AltUnityTextureInformation ReceiveImage() {

            var data = Recvall();
            if (data == "Ok") {
                data = Recvall();
            } else {
                throw new System.Exception("Out of order operations");
            }
            string[] screenshotInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(data);

            // Some workaround this: https://stackoverflow.com/questions/710853/base64-string-throwing-invalid-character-error
            var screenshotParts = screenshotInfo[4].Split('\0');
            screenshotInfo[4] = "";
            for (int i = 0; i < screenshotParts.Length; i++) {
                screenshotInfo[4] += screenshotParts[i];
            }

            var scaleDifference = screenshotInfo[0];

            var length = screenshotInfo[1];
            var textureFormatString = screenshotInfo[2];
            var textureFormat = (AltUnityTextureFormat)System.Enum.Parse(typeof(AltUnityTextureFormat), textureFormatString);
            var textSizeString = screenshotInfo[3];
            var textSizeVector3 = Newtonsoft.Json.JsonConvert.DeserializeObject<AltUnityVector3>(textSizeString);

            byte[] imageCompressed = Newtonsoft.Json.JsonConvert.DeserializeObject<byte[]>(screenshotInfo[4], new Newtonsoft.Json.JsonSerializerSettings {
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii
            });

            byte[] imageDecompressed = DeCompressScreenshot(imageCompressed);

            return new AltUnityTextureInformation(imageDecompressed, Newtonsoft.Json.JsonConvert.DeserializeObject<AltUnityVector2>(scaleDifference), textSizeVector3, textureFormat);
        }
        public static byte[] DeCompressScreenshot(byte[] screenshotCompressed) {

            using (var memoryStreamInput = new System.IO.MemoryStream(screenshotCompressed))
            using (var memoryStreamOutput = new System.IO.MemoryStream()) {
                using (var gs = new System.IO.Compression.GZipStream(memoryStreamInput, System.IO.Compression.CompressionMode.Decompress)) {
                    CopyTo(gs, memoryStreamOutput);
                }

                return memoryStreamOutput.ToArray();
            }
        }
        public static T[] SubArray<T>(T[] data, int index, long length) {
            T[] result = new T[length];
            System.Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static void CopyTo(System.IO.Stream src, System.IO.Stream dest) {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
                dest.Write(bytes, 0, cnt);
            }
        }
    }
}