using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetAllMethodsCommand : AltUnityReflectionMethodsCommand {
        TestComponent component;
        AltUnityMethodSelection methodSelection;

        public AltUnityGetAllMethodsCommand(TestComponent component, AltUnityMethodSelection methodSelection) {
            this.component = component;
            this.methodSelection = methodSelection;
        }

        public override string Execute() {
            AltUnityRunner._altUnityRunner.LogMessage("getAllMethods");
            System.Type type = GetType(component.componentName, component.assemblyName);
            MethodInfo[] methodInfos = new MethodInfo[1];
            switch (methodSelection) {

                case AltUnityMethodSelection.CLASSMETHODS:
                    methodInfos = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    break;

                case AltUnityMethodSelection.INHERITEDMETHODS:
                    var allMethods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    var classMethods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    methodInfos = allMethods.Except(classMethods).ToArray();
                    break;

                case AltUnityMethodSelection.ALLMETHODS:
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
