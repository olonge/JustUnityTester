using System.Collections.Generic;
using JustUnityTester.Core;
using Newtonsoft.Json;

namespace JustUnityTester.Server.Commands {
    class AltUnityGetAllComponentsCommand : AltUnityCommand {
        string objectID;

        public AltUnityGetAllComponentsCommand(string objectID) {
            this.objectID = objectID;
        }

        public override string Execute() {
            TestRunner.Instance.LogMessage("GetAllComponents");

            UnityEngine.GameObject obj = TestRunner.GetGameObject(System.Convert.ToInt32(objectID));
            List<TestComponent> listComponents = new List<TestComponent>();

            foreach (var component in obj.GetComponents<UnityEngine.Component>()) {
                try {
                    var a = component.GetType();
                    var componentName = a.FullName;
                    var assemblyName = a.Assembly.GetName().Name;
                    listComponents.Add(new TestComponent(componentName, assemblyName));

                } catch (System.NullReferenceException e) {

                    if (e.Source != null)
                        UnityEngine.Debug.LogError("NullReferenceException source: " + e.Source);
                    else
                        UnityEngine.Debug.LogError("NullReferenceException unknown source");
                }
            }

            var response = JsonConvert.SerializeObject(listComponents);
            return response;
        }
    }
}
