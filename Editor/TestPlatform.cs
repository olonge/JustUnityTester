namespace JustUnityTester.Editor {
    public enum TestPlatform {
        Android,
#if UNITY_EDITOR_OSX
    iOS,
#endif
        Editor,
        Standalone
    }
}