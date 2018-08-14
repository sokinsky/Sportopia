using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data {
    public interface IReader<out TContext, out TModel> where TContext: Context where TModel : Model {
        IController<TContext, TModel> Controller { get; }
        TContext Context { get; }
        TModel Model { get; }
        JToken Read(PropertyInfo propertyInfo, JToken jToken);
    }
    public class Reader<TContext, TModel> : IReader<TContext, TModel> where TContext : Context where TModel : Model {
        public Reader(IController<TContext, TModel> controller) {
            this.Controller = controller;
        }
        public IController<TContext, TModel> Controller { get; }
        public TContext Context { get; }
        public TModel Model { get; }

        public JToken Read(PropertyInfo propertyInfo, JToken jToken) {
            return jToken;
        }
    }
}
