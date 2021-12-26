using System;

namespace JustUnityTester.Editor {
    [Serializable]
    public class MyTest {

        public bool _selected;
        public string _testName;
        public int _status;
        public bool _isSuite;
        public Type _type;
        public string _parentName;
        public int _testCaseCount;
        public bool _foldOut;
        public string _testResultMessage;
        public string _testStackTrace;
        public double _testDuration;
        public string path;

        public MyTest(bool selected, string testName, int status, bool isSuite, Type type, string parentName, int testCaseCount, bool foldOut, string testResultMessage, string testStackTrace, double testDuration, string path) {
            _selected = selected;
            _testName = testName;
            _status = status;
            _isSuite = isSuite;
            _type = type;
            _parentName = parentName;
            _testCaseCount = testCaseCount;
            _foldOut = foldOut;
            _testResultMessage = testResultMessage;
            _testStackTrace = testStackTrace;
            _testDuration = testDuration;
            this.path = path;
        }

        public bool Selected { get => _selected; set => _selected = value; }
        public string TestName { get => _testName; set => _testName = value; }
        public int Status { get => _status; set => _status = value; }
        public bool IsSuite { get => _isSuite; set => _isSuite = value; }
        public Type Type { get => _type; set => _type = value; }
        public string ParentName { get => _parentName; set => _parentName = value; }
        public int TestCaseCount { get => _testCaseCount; set => _testCaseCount = value; }
        public bool FoldOut { get => _foldOut; set => _foldOut = value; }
        public string TestResultMessage { get => _testResultMessage; set => _testResultMessage = value; }
        public string TestStackTrace { get => _testStackTrace; set => _testStackTrace = value; }
        public double TestDuration { get => _testDuration; set => _testDuration = value; }
        public string Path { get => path; set => path = value; }
    }
}