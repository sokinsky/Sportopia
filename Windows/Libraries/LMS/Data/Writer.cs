using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data {
    public interface IWriter<out TContext, out TModel>  where TContext:Context where TModel : Model {
        IController<TContext, TModel> Controller { get; }

        JToken Write();
        JToken Write(PropertyInfo propertyInfo);
    }
    public class Writer<TContext, TModel> : IWriter<TContext, TModel> where TContext : Context where TModel:Model {
        public Writer(IController<TContext, TModel> controller) {
            this.Controller = controller;
        }
        public IController<TContext, TModel> Controller { get; }
        public Context Context {
            get {
                return this.Controller.Context;
            }
        }
        public Model Model {
            get {
                return this.Controller.Model;
            }
        }

        public virtual JToken Write() {
            if (this.Controller.State() != System.Data.Entity.EntityState.Unchanged)
                throw new Exception("Changed Data has not been saved");

            JObject result = new JObject();
            foreach (PropertyInfo propertyInfo in this.Model.GetType().GetProperties()) {
                JToken propertyValue = this.Write(propertyInfo);
                if (propertyValue != null)
                    result[propertyInfo.Name] = propertyValue;
            }
            return result;
        }
        public virtual JToken Write(PropertyInfo propertyInfo) {
            object propertyValue = propertyInfo.GetValue(this.Model);
            if (propertyValue == null)
                return null;

            if (propertyValue.GetType().IsAssignableFrom(typeof(Model)))
                return JToken.FromObject(((Model)propertyValue).ID);
            else if (propertyValue.GetType().IsAssignableFrom(typeof(IEnumerable<Model>)))
                return null;
            else
                return JToken.FromObject(propertyValue);
        }
    }
}
