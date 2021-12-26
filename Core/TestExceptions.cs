namespace JustUnityTester.Exceptions {
    public class TestException : System.Exception {
        public TestException() {

        }

        public TestException(string message) : base(message) {

        }
    }

    public class NotFoundException : TestException {
        public NotFoundException() {
        }

        public NotFoundException(string message) : base(message) {
        }
    }

    public class PropertyNotFoundException : TestException {
        public PropertyNotFoundException() {
        }

        public PropertyNotFoundException(string message) : base(message) {
        }
    }

    public class MethodNotFoundException : TestException {
        public MethodNotFoundException() {
        }

        public MethodNotFoundException(string message) : base(message) {
        }
    }

    public class ComponentNotFoundException : TestException {
        public ComponentNotFoundException() {
        }

        public ComponentNotFoundException(string message) : base(message) {
        }
    }

    public class CouldNotPerformOperationException : TestException {
        public CouldNotPerformOperationException() {
        }

        public CouldNotPerformOperationException(string message) : base(message) {
        }
    }

    public class IncorrectNumberOfParametersException : TestException {
        public IncorrectNumberOfParametersException() {
        }

        public IncorrectNumberOfParametersException(string message) : base(message) {
        }
    }

    public class CouldNotParseJsonStringException : TestException {
        public CouldNotParseJsonStringException() {
        }

        public CouldNotParseJsonStringException(string message) : base(message) {
        }
    }

    public class FailedToParseArgumentsException : TestException {
        public FailedToParseArgumentsException() {
        }

        public FailedToParseArgumentsException(string message) : base(message) {
        }
    }

    public class ObjectWasNotFoundException : TestException {
        public ObjectWasNotFoundException() {
        }

        public ObjectWasNotFoundException(string message) : base(message) {
        }
    }

    public class PropertyCannotBeSetException : TestException {
        public PropertyCannotBeSetException() {
        }

        public PropertyCannotBeSetException(string message) : base(message) {
        }
    }

    public class NullReferenceException : TestException {
        public NullReferenceException() {
        }

        public NullReferenceException(string message) : base(message) {
        }
    }

    public class UnknownErrorException : TestException {
        public UnknownErrorException() {
        }

        public UnknownErrorException(string message) : base(message) {
        }
    }

    public class FormatException : TestException {
        public FormatException() {
        }

        public FormatException(string message) : base(message) {
        }
    }

    public class WaitTimeOutException : TestException {
        public WaitTimeOutException() {
        }

        public WaitTimeOutException(string message) : base(message) {
        }
    }

}
