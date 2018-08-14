using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data {
    public interface IValidator<out TContext, out TModel> where TContext : Context where TModel : Model {
        IController<TContext, TModel> Controller { get; }
        Message Validate();
    }
    public class Validator<TContext, TModel> : IValidator<TContext, TModel> where TContext : Context where TModel:Model {
        public Validator(Controller<TContext, TModel> controller) {
            this.Controller = controller;
        }
        public IController<TContext, TModel> Controller { get; }
        public TContext Context { get { return this.Controller.Context; } }
        public TModel Model { get { return this.Controller.Model; } }


        public virtual Message Validate() {
            Type modelType = this.Controller.Model.GetType();
            if (modelType.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                modelType = modelType.BaseType;

            Message result = new Message {
                Name = $"{modelType.Name}({this.Model.ID})",
                Action = this.Controller.State()
            };               
            List<Message> innerMessages = new List<Message>();
            foreach (PropertyInfo propertyInfo in this.Controller.Model.GetType().GetProperties()) {
                Message propertyMessage = this.Validate(propertyInfo);
                propertyMessage.Action = this.Controller.State(propertyInfo);
                propertyMessage.Name = propertyInfo.Name;
                innerMessages.Add(propertyMessage);
            }

            if (innerMessages.Count > 0) {
                result.InnerMessages = innerMessages.Where(x => x.Type != MessageType.OK || x.Action != System.Data.Entity.EntityState.Unchanged).ToList();
                if (innerMessages.Count(x => x.Type == MessageType.Invalid) > 0)
                    result.Type = MessageType.Invalid;
                else if (innerMessages.Count(x => x.Type == MessageType.Unauthorized) > 0)
                    result.Type = MessageType.Unauthorized;
                else
                    result.Type = MessageType.OK;
            }
            return result;
        }
        public virtual Message Validate(PropertyInfo propertyInfo) {
            return Message.OK(propertyInfo.Name);
        }
    }
}
