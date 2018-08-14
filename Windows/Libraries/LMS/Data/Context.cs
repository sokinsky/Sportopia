using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.Data {
    public class Context : DbContext {
        public Context(string connectionName) : base(connectionName) {}

        public IRepository<Context, Model> this[string type] {
            get {
                string assemblyName = this.GetType().Assembly.GetName().Name;
                string modelsNamespace = $"{assemblyName}.Data.Models.";
                if (!type.StartsWith(modelsNamespace))
                    type = $"{modelsNamespace}{type}";
                return this[this.GetType().Assembly.GetType(type)];
            }
        }
        public IRepository<Context, Model> this[Type type] {
            get {
                if (type.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                    type = type.BaseType;

                Type repositoryType = type.Assembly.GetType(Regex.Replace(type.FullName, @"\.Models\.", ".Repositories."));
                if (repositoryType != null)
                    return (IRepository<Context, Model>)repositoryType.GetConstructor(new Type[] { this.GetType() }).Invoke(new object[] { this });
                else {
                    repositoryType = typeof(Repository<,>).MakeGenericType(this.GetType(), type);
                    return (IRepository<Context, Model>)Activator.CreateInstance(repositoryType, new object[] { this });
                }
            }
        }

        public IController<Context, Model> this[string type, JToken jToken] {
            get {
                return this[type].Select(jToken);
            }
        }
        public IController<Context, Model> this[Type type, JToken jToken] {
            get {
                return this[type].Select(jToken);
            }
        }
        public IController<Context, Model> this[Model model] {
            get {
                Type modelType = model.GetType();
                if (modelType.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                    modelType = modelType.BaseType;

                Type controllerType = model.GetType().Assembly.GetType(Regex.Replace(modelType.FullName, @"\.Models\.", ".Contollers."));
                if (controllerType != null)
                    return (IController<Context, Model>)controllerType.GetConstructor(new Type[] { this.GetType(), modelType}).Invoke(new object[] { this, model });
                else {
                    controllerType = typeof(Controller<,>).MakeGenericType(this.GetType(), modelType);
                    return (IController<Context, Model>)Activator.CreateInstance(controllerType, new object[] { this, model });
                }
            }
        }
        public IController<Context, Model> this[Bridge.Model bridgeModel] {
            get {
                if (bridgeModel.Controller != null)
                    return bridgeModel.Controller;
                bridgeModel.Controller = this[bridgeModel.Type, bridgeModel.Value];
                if (bridgeModel.Controller == null)
                    bridgeModel.Controller = this[bridgeModel.Type].Create(bridgeModel.Value);
                return bridgeModel.Controller;
            }            
        }

        public Bridge.Context Bridge { get; set; }

        public bool SaveChanges(Bridge.Context bridgeContext) {
            this.Bridge = bridgeContext;
            foreach (Bridge.Model bridgeModel in bridgeContext.Models) {
                if (bridgeModel.Controller == null)
                    bridgeModel.Controller = this[bridgeModel];
            }
            foreach (Bridge.Model bridgeModel in bridgeContext.Models) {
                bridgeModel.UpdateChanges(bridgeContext);
            }
            this.ValidateChanges(bridgeContext);
            if (bridgeContext.Models.Count(x => x.Message.Type != MessageType.OK) > 0) 
                return false;

            this.SaveChanges();
            foreach (Bridge.Model bridgeModel in bridgeContext.Models) {
                bridgeModel.RefreshValue();
            }
            return true;
        }
        public void ValidateChanges(Bridge.Context bridgeContext) {
            foreach (var entry in this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged)) {
                Bridge.Model bridgeModel = bridgeContext.Models.FirstOrDefault(x => x.Controller.Model == (Data.Model)entry.Entity);
                if (bridgeModel == null) {
                    bridgeModel = new Bridge.Model(this[(Data.Model)entry.Entity]);
                    bridgeContext.Models.Add(bridgeModel);
                }
                    
                bridgeModel.ValidateChanges();
            }
        }
    }
}
