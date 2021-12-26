namespace JustUnityTester.Editor {
    [System.Serializable]
    public class MyScenes {
        public bool _toBeBuilt;
        public string _path;
        public int _buildIndex;

        public MyScenes(bool beBuilt, string path, int buildIndex) {
            _toBeBuilt = beBuilt;
            _path = path;
            _buildIndex = buildIndex;
        }

        public bool ToBeBuilt {
            get => _toBeBuilt;
            set => _toBeBuilt = value;
        }

        public string Path {
            get => _path;
            set => _path = value;
        }

        public int BuildScene {
            get => _buildIndex;
            set => _buildIndex = value;
        }
    }
}