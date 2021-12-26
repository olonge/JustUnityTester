using System.Linq;

namespace JustUnityTester.Server {
    public interface ISocketDelegatable {
        // callback, will be NOT be invoked on main thread, so make sure to synchronize in Unity
        void ReadMessage(ClientSocket socket, string message);
    }

    public class ClientSocket {

        protected readonly System.Net.Sockets.TcpClient Client;
        protected readonly string SeparatorSequence;
        protected readonly char[] SeparatorSequenceChars;
        protected readonly System.Text.Encoding Encoding;
        protected ISocketDelegatable iClientSocketDelegatable;
        public bool ToBeKilled;


        public ClientSocket(System.Net.Sockets.TcpClient client,
                                        ISocketDelegatable iClientSocketDelegatable,
                                        string separatorString,
                                        System.Text.Encoding encoding) {
            Client = client;
            Encoding = encoding;
            SeparatorSequence = separatorString;
            SeparatorSequenceChars = separatorString.ToCharArray();
            this.iClientSocketDelegatable = iClientSocketDelegatable;
            ToBeKilled = false;
        }

        public void Cleanup() {
            if (Client != null) {
                ToBeKilled = true;
                Client.Close();
            }

        }

        public void SendResponse(string response) {
            TestRunner.logMessage = System.Text.RegularExpressions.Regex.Replace(TestRunner.logMessage, @"\r\n|\n|\r$", "");//Removes the last new line
            response = "altstart::" + response + "::altLog::" + TestRunner.logMessage + "::altend";
            TestRunner.logMessage = "";
            TestRunner.FileWriter.WriteLine(System.DateTime.Now + ": sending response: " + response);
            Client.Client.Send(Encoding.GetBytes(response));
        }
        public void SendResponse(byte[] response) {
            response = Encoding.GetBytes("altstart::").Concat(response).Concat(Encoding.GetBytes("::altLog::")).Concat(Encoding.GetBytes(TestRunner.logMessage)).Concat(Encoding.GetBytes("::altend")).ToArray();
            UnityEngine.Debug.Log("sending response: " + System.Text.Encoding.ASCII.GetString(response));
            Client.Client.Send(response);
        }

        public void Run() {
            try {
                System.Text.StringBuilder dataBuffer = new System.Text.StringBuilder();

                while (true) {
                    byte[] readBuffer = new byte[256];
                    int readLength = Client.Client.Receive(readBuffer);

                    // append to token
                    if (readLength > 0) {
                        dataBuffer.Append(Encoding.GetString(readBuffer, 0, readLength));
                        string data = dataBuffer.ToString();
                        dataBuffer = new System.Text.StringBuilder();

                        string[] tokens = data.Split(new[] { SeparatorSequence }, System.StringSplitOptions.None);

                        bool endsWithSeparator = data.EndsWith(SeparatorSequence);

                        // all except the last piece
                        for (int i = 0; i < tokens.Length - 1; i++) {
                            iClientSocketDelegatable.ReadMessage(this, tokens[i]);
                        }

                        // for the last piece, if the data ended with separator than this is a full token
                        // otherwise, its not so append to data buffer
                        if (endsWithSeparator) {
                            if (tokens[tokens.Length - 1].Length > 0) {
                                iClientSocketDelegatable.ReadMessage(this, tokens[tokens.Length - 1]);
                            }
                        } else {
                            dataBuffer.Append(tokens[tokens.Length - 1]);
                        }
                    }

                    if (ToBeKilled) {
                        break;
                    }
                }
            } catch (System.Threading.ThreadAbortException exception) {
                TestRunner.FileWriter.WriteLine("Thread aborted(" + exception + ")");
                UnityEngine.Debug.Log("Thread aborted(" + exception + ")");
            } catch (System.Net.Sockets.SocketException exception) {
                TestRunner.FileWriter.WriteLine("Socket exception(" + exception + ")");
                UnityEngine.Debug.Log("Socket exception(" + exception + ")");
            } catch (System.Exception exception) {
                TestRunner.FileWriter.WriteLine("Exception(" + exception + ")");
                UnityEngine.Debug.Log("Exception(" + exception + ")");
            } finally {
                Client.Close();
                ToBeKilled = true;
                TestRunner.FileWriter.WriteLine("AltClientSocketHandler - Client closed");
                UnityEngine.Debug.Log("AltClientSocketHandler - Client closed");

            }
        }

    }
}