namespace JustUnityTester.Server.Commands {
    public abstract class Command {
        public void SendResponse(AltClientSocketHandler handler) {
            TestRunner._responseQueue.ScheduleResponse(delegate {
                string response = null;
                try {
                    response = Execute();
                } catch (System.NullReferenceException exception) {
                    UnityEngine.Debug.Log(exception);
                    response = TestRunner.Instance.errorNullRefferenceMessage;
                } catch (System.ArgumentException exception) {
                    UnityEngine.Debug.Log(exception);
                    response = TestRunner.Instance.errorFailedToParseArguments;
                } catch (System.Reflection.TargetParameterCountException) {
                    response = TestRunner.Instance.errorIncorrectNumberOfParameters;
                } catch (Newtonsoft.Json.JsonException e) {
                    UnityEngine.Debug.Log(e);
                    response = TestRunner.Instance.errorCouldNotParseJsonString;
                } catch (Exceptions.ComponentNotFoundException e) {
                    UnityEngine.Debug.Log(e);
                    response = TestRunner.Instance.errorComponentNotFoundMessage;
                } catch (Exceptions.PropertyNotFoundException e) {
                    UnityEngine.Debug.Log(e);
                    response = TestRunner.Instance.errorPropertyNotFoundMessage;
                } catch (System.Exception exception) {
                    UnityEngine.Debug.Log(exception);
                    response = TestRunner.Instance.errorUnknownError + TestRunner.Instance.requestSeparatorString + exception;
                } finally {
                    handler.SendResponse(response);
                }
            });
        }
        public abstract string Execute();

    }
}