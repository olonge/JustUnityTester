namespace JustUnityTester.Core {
    public struct TestComponent {
        public string componentName;
        public string assemblyName;

        public TestComponent(string componentName, string assemblyName) {
            this.componentName = componentName;
            this.assemblyName = assemblyName;
        }
    }
}