using System;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

namespace JustUnityTester.Server.Commands {
    public abstract class Command {
        public void SendResponse(ClientSocket handler) {
            TestRunner._responseQueue.ScheduleResponse(delegate {
                string response = null;
                try {
                    response = Execute();

                } catch (NullReferenceException exception) {
                    Debug.Log(exception);
                    response = TestRunner.Instance.errorNullRefferenceMessage;

                } catch (ArgumentException exception) {
                    Debug.Log(exception);
                    response = TestRunner.Instance.errorFailedToParseArguments;

                } catch (TargetParameterCountException) {
                    response = TestRunner.Instance.errorIncorrectNumberOfParameters;

                } catch (JsonException e) {
                    Debug.Log(e);
                    response = TestRunner.Instance.errorCouldNotParseJsonString;

                } catch (Exceptions.ComponentNotFoundException e) {
                    Debug.Log(e);
                    response = TestRunner.Instance.errorComponentNotFoundMessage;

                } catch (Exceptions.PropertyNotFoundException e) {
                    Debug.Log(e);
                    response = TestRunner.Instance.errorPropertyNotFoundMessage;

                } catch (Exception exception) {
                    Debug.Log(exception);
                    response = TestRunner.Instance.errorUnknownError + TestRunner.Instance.requestSeparatorString + exception;

                } finally {
                    handler.SendResponse(response);
                }
            });
        }
        public abstract string Execute();

    }
}