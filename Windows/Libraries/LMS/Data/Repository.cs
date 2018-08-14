using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.Data {
    public interface IRepository<out TContext, out TModel> where TContext : Context where TModel : Model,new() {
        TContext Context { get; }
        IController<TContext, TModel> Create(JToken jToken);
        IController<TContext, TModel> Create(Model model);
        IController<TContext, TModel> Select(JToken jToken);
    }
    public class Repository<TContext, TModel> : IRepository<TContext, TModel> where TContext : Context where TModel : Model,new() {
        public Repository(TContext context) {
            this.Context = context;
        }
        public TContext Context { get; }

        public virtual IController<TContext, TModel> Create(JToken jToken) {
            switch (jToken) {
                case JObject jObject:
                    return this.Create(jObject);
                default:
                    return this.Create(new TModel());
            }
        }
        public virtual IController<TContext, TModel> Create(JObject jObject) {
            return this.Create(new TModel());
        }
        public virtual IController<TContext, TModel> Create(Model model) {
            var dbset = this.Context.Set<TModel>();
            dbset.Add((TModel)model);
            return this.createController((TModel)model);
        }

        public virtual IController<TContext, TModel> Select(JToken jToken) {
            switch (jToken) {
                case JValue jValue:
                    Guid guid = new Guid();
                    if (Guid.TryParse(jToken.ToString(), out guid)) {
                        Bridge.Model bridgeModel = this.Context.Bridge[guid];
                        if (bridgeModel != null) {
                            if (bridgeModel.Controller == null)
                                bridgeModel.Controller = this.Context[bridgeModel];
                            return (IController<TContext, TModel>)bridgeModel.Controller;
                        }          
                    }
                    return this.Select(jValue);
                case JObject jObject:
                    return this.Select(jObject);
                default:
                    throw new Exception("Invalid Token");
            }
        }
        public virtual IController<TContext, TModel> Select(JValue jValue) {
            int id = 0;
            if (int.TryParse(jValue.ToString(), out id)) {
                TModel model = ((IEnumerable<TModel>)this.Context.Set(typeof(TModel))).FirstOrDefault(x => x.ID == id);
                if (model != null)
                    return this.createController(model);
            }
            return null;                
        }
        public virtual IController<TContext, TModel> Select(JObject jObject) {
            if (jObject.ContainsKey("ID") && jObject["ID"] != null && jObject["ID"].GetType() == typeof(JValue))
                return Select(jObject["ID"]);
            return null;                
        }

        
        protected IController<TContext, TModel> createController(TModel model) {
            Type modelType = model.GetType();
            if (modelType.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                modelType = modelType.BaseType;

            Type controllerType = model.GetType().Assembly.GetType(Regex.Replace(modelType.FullName, @"\.Models\.", ".Controllers."));
            if (controllerType != null)
                return (IController<TContext, TModel>)controllerType.GetConstructor(new Type[] { this.Context.GetType(), modelType }).Invoke(new object[] { this.Context, model });
            else {
                controllerType = typeof(Controller<,>).MakeGenericType(this.Context.GetType(), modelType);
                return (IController<TContext, TModel>)Activator.CreateInstance(controllerType, new object[] { this.Context, model });
            }
        }
    }
}
