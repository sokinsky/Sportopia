using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.Data {
    public interface IController<out TContext, out TModel> where TContext:Context where TModel : Model {
        TContext Context { get; }
        TModel Model { get; }
        int? ID { get; }

        void Update(JToken jToken);
        void Update(PropertyInfo propertyInfo, JToken jToken);
        void Delete();

        JToken Read(PropertyInfo propertyInfo, JToken jToken);
        JToken Write();
        JToken Write(PropertyInfo propertyInfo);

        EntityState State();
        EntityState State(PropertyInfo propertyInfo);

        Message Validate();
    }
    public class Controller<TContext, TModel> : IController<TContext, TModel> where TContext:Context where TModel : Model {
        public Controller(TContext context, TModel model) {
            this.Context = context;
            this.Model = model;
        }
        public TContext Context { get; }
        public TModel Model { get; }
        public int? ID { get { return this.Model?.ID; } }

        private IValidator<TContext, TModel> __validator;
        public IValidator<TContext, TModel> Validator {
            get {
                if (this.__validator == null) {
                    Type modelType = this.Model.GetType();
                    if (modelType.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                        modelType = modelType.BaseType;

                    Type validatorType = this.Model.GetType().Assembly.GetType(Regex.Replace(modelType.FullName, @"\.Models\.", ".Validators."));
                    if (validatorType != null)
                        this.__validator =(IValidator<TContext, TModel>)validatorType.GetConstructor(new Type[] { this.GetType() }).Invoke(new object[] { this });
                    else {
                        validatorType = typeof(Validator<,>).MakeGenericType(this.Context.GetType(), modelType);
                        this.__validator = (IValidator<TContext, TModel>)Activator.CreateInstance(validatorType, new object[] { this });
                    }
                }
                return this.__validator;
            }
        }

        private IAuthorizer<TContext, TModel> __authorizer;
        public IAuthorizer<TContext, TModel> Authorizer {
            get {
                if (this.__authorizer == null) {
                    Type authorizerType = typeof(TModel).Assembly.GetType(Regex.Replace(typeof(TModel).FullName, @"\.Models\.", ".Authorizers."));
                    if (authorizerType != null)
                        this.__authorizer = (IAuthorizer<TContext, TModel>)authorizerType.GetConstructor(new Type[] { this.GetType() }).Invoke(new object[] { this });
                    else {
                        authorizerType = typeof(Authorizer<,>).MakeGenericType(this.Context.GetType(), this.Model.GetType());
                        this.__authorizer = (IAuthorizer<TContext, TModel>)Activator.CreateInstance(authorizerType, new object[] { this });
                    }
                }
                return this.__authorizer;
            }
        }

        private IReader<TContext, TModel> __reader;
        public IReader<TContext, TModel> Reader {
            get {
                if (this.__reader == null) {
                    Type modelType = this.Model.GetType();
                    if (modelType.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                        modelType = modelType.BaseType;

                    Type readerType = typeof(TModel).Assembly.GetType(Regex.Replace(typeof(TModel).FullName, @"\.Models\.", ".Readers."));
                    if (readerType != null)
                        this.__reader = (IReader<TContext, TModel>)readerType.GetConstructor(new Type[] { this.GetType() }).Invoke(new object[] { this.GetType() });
                    else {
                        readerType = typeof(Reader<,>).MakeGenericType(this.Context.GetType(), modelType);
                        this.__reader = (IReader<TContext, TModel>)Activator.CreateInstance(readerType, new object[] { this });
                    }
                }
                return this.__reader;
            }
        }

        private IWriter<TContext, TModel> __writer;
        public IWriter<TContext, TModel> Writer {
            get {
                if (this.__writer == null) {
                    Type writerType = typeof(TModel).Assembly.GetType(Regex.Replace(typeof(TModel).FullName, @"\.Models\.", ".Writers."));
                    if (writerType != null)
                        this.__writer = (IWriter<TContext, TModel>)writerType.GetConstructor(new Type[] { this.GetType() }).Invoke(new object[] { this });
                    else {
                        writerType = typeof(Writer<,>).MakeGenericType(this.Context.GetType(), this.Model.GetType());
                        return (IWriter<TContext, TModel>)Activator.CreateInstance(writerType, new object[] { this });
                    }
                }
                return this.__writer;
            }
        }

        public void Update(JToken jToken) {
            switch (jToken) {
                case JValue jValue:
                    this.Update(jValue);
                    break;
                case JObject jObject:
                    this.Update(jObject);
                    break;
                case JArray jArray:
                    this.Update(jArray);
                    break;
                default:
                    throw new Exception("Invalid Update Token");
            }
        }
        public virtual void Update(JValue jValue) {
            throw new Exception("Invalid Update Token");
        }
        public virtual void Update(JObject jObject) {
            foreach (JProperty jProperty in jObject.Properties()) {
                PropertyInfo propertyInfo = this.Model.GetType().GetProperty(jProperty.Name);
                if (propertyInfo != null)
                    this.Update(propertyInfo, jObject[jProperty.Name]);
            }
        }
        protected virtual void Update(JArray jArray) {
            throw new Exception("Invalid Update Token");
        }

        public virtual void Update(PropertyInfo propertyInfo, JToken jToken) {
            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(IEnumerable<Data.Model>)))
                return;

            jToken = this.Reader.Read(propertyInfo, jToken);
            if (typeof(Data.Model).IsAssignableFrom(propertyInfo.PropertyType)) {
                var modelController = this.Context[propertyInfo.PropertyType, jToken];
                if (modelController != null)
                    propertyInfo.SetValue(this.Model, modelController.Model);
            }
            else
                propertyInfo.SetValue(this.Model, jToken.ToObject(propertyInfo.PropertyType));
        }

        public void Delete() {
            this.Context.Set<TModel>().Remove(this.Model);
        }

        public Message Validate() {
            return this.Validator.Validate();
        }

        public EntityState State() {
            return this.Context.Entry(this.Model).State;
        }
        public EntityState State(PropertyInfo propertyInfo) {
            switch (this.State()) {
                case EntityState.Added:
                    if (propertyInfo.GetValue(this.Model) != null)
                        return EntityState.Added;
                    else
                        return EntityState.Unchanged;
                case EntityState.Deleted:
                    if (propertyInfo.GetValue(this.Model) != null)
                        return EntityState.Deleted;
                    else
                        return EntityState.Unchanged;
                case EntityState.Modified:
                    if (this.Context.Entry(this.Model).OriginalValues.PropertyNames.Contains(propertyInfo.Name)) {
                        object originalValue = this.Context.Entry(this.Model).OriginalValues[propertyInfo.Name];
                        object newValue = propertyInfo.GetValue(this.Model);
                        if (newValue == null) {
                            if (originalValue == null)
                                return EntityState.Unchanged;
                            else
                                return EntityState.Deleted;
                        }
                        else {
                            if (originalValue == null)
                                return EntityState.Added;
                            else if (!originalValue.Equals(newValue))
                                return EntityState.Modified;
                            else
                                return EntityState.Unchanged;
                        }
                    }
                    break;

            }
            return EntityState.Unchanged;
        }

        public virtual JToken Read(PropertyInfo propertyInfo, JToken jToken) {
            return jToken;
        }
        public virtual JToken Write() {
            JObject result = new JObject();
            foreach (PropertyInfo propertyInfo in this.Model.GetType().GetProperties()) {
                JToken jToken = this.Write(propertyInfo);
                if (jToken != null)
                    result[propertyInfo.Name] = jToken;
            }
            return result;
        }
        public virtual JToken Write(PropertyInfo propertyInfo) {
            object propertyValue = propertyInfo.GetValue(this.Model);
            if (propertyValue == null)
                return null;

            switch (propertyValue) {
                case Model reference:
                    return JToken.FromObject(reference.ID);
                case IEnumerable<Model> referenceCollection:
                    return null;
                default:
                    return JToken.FromObject(propertyValue);
            }
        }
    }
}
