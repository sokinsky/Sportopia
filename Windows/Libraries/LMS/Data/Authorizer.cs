using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data {
    public interface IAuthorizer<out TContext, out TModel> where TContext:Context where TModel : Model {
        IController<TContext, TModel> Controller { get; }
        TContext Context { get; }
        TModel Model { get; }
    }
    public class Authorizer<TContext, TModel> : IAuthorizer<TContext, TModel> where TContext : Context where TModel : Model {
        public Authorizer(Controller<TContext, TModel> controller) {
            this.Controller = controller;
        }
        public IController<TContext, TModel> Controller { get; }
        public TContext Context {
            get {
                return this.Controller.Context;
            }
        }
        public TModel Model {
            get {
                return this.Controller.Model;
            }
        }
    }
}
