using System.Collections.Generic;
using System.Reflection;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetAllFieldsCommand : AltUnityReflectionMethodsCommand {
        string id;
        TestComponent component;

        public AltUnityGetAllFieldsCommand(string id, TestComponent component) {
            this.id = id;
            this.component = component;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("getAllFields");
            UnityEngine.GameObject obj;

            obj = id.Equals("null") ? null : TestRunner.GetGameObject(System.Convert.ToInt32(id));
            System.Type type = GetType(component.componentName, component.assemblyName);

            var altObjectComponent = obj.GetComponent(type);
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var listFields = new List<TestField>();

            foreach (var fieldInfo in fieldInfos) {
                var value = fieldInfo.GetValue(altObjectComponent);
                var field = new TestField(fieldInfo.Name, value == null ? "null" : value.ToString());

                listFields.Add(field);
            }
            return JsonConvert.SerializeObject(listFields);
        }
    }
}
