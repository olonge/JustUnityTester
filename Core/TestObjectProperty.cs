namespace JustUnityTester.Core {
    public struct TestObjectProperty {
        public string Component;
        public string Property;
        public string Assembly;

        public TestObjectProperty(string component = "", string property = "") :
            this(component, property, null) { }

        public TestObjectProperty(string component, string property, string assembly) {
            Component = component;
            Property = property;
            Assembly = assembly;
        }
    }
}