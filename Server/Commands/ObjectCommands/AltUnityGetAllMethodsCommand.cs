using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetAllMethodsCommand : AltUnityReflectionMethodsCommand {
        TestComponent component;
        TestMethodSelection methodSelection;

        public AltUnityGetAllMethodsCommand(TestComponent component, TestMethodSelection methodSelection) {
            this.component = component;
            this.methodSelection = methodSelection;
        }

        public override string Execute() {
            TestRunner._altUnityRunner.LogMessage("getAllMethods");
            System.Type type = GetType(component.componentName, component.assemblyName);
            MethodInfo[] methodInfos = new MethodInfo[1];
            switch (methodSelection) {

                case TestMethodSelection.CLASSMETHODS:
                    methodInfos = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    break;

                case TestMethodSelection.INHERITEDMETHODS:
                    var allMethods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    var classMethods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    methodInfos = allMethods.Except(classMethods).ToArray();
                    break;

                case TestMethodSelection.ALLMETHODS:
                    methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    break;
            }

            var listMethods = new List<string>();

            foreach (var methodInfo in methodInfos) {
                listMethods.Add(methodInfo.ToString());
            }
            return JsonConvert.SerializeObject(listMethods);
        }
    }
}
