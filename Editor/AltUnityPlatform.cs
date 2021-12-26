namespace JustUnityTester.Editor {
    public enum AltUnityPlatform {
        Android,
#if UNITY_EDITOR_OSX
    iOS,
#endif
        Editor,
        Standalone
    }
}