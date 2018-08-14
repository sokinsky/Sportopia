using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Bridge {
    public class Model {
        public Model() {
            this.ID = Guid.NewGuid();
        }
        public Model(IController<Data.Context, Data.Model> controller) {
            this.ID = Guid.NewGuid();            
            this.Controller = controller;
            this.Type = this.Controller.Model.GetType().Name;
        }

        [JsonConverter(typeof(Json.Converters.GuidConverter))]
        public Guid? ID { get; set; }
        public string Type { get; set; }
        public JToken Value { get; set; }
        public Message Message { get; set; } = new Message {
            Type = MessageType.OK,
            Action = System.Data.Entity.EntityState.Unchanged
        };

        [JsonIgnore]
        public IController<Data.Context, Data.Model> Controller { get; set; }
        [JsonIgnore]
        public string BridgeID {
            get {
                if (this.Controller?.Model?.ID != null)
                    return $"{this.Type}({this.Controller.Model.ID})";
                else
                    return $"{this.Type}({this.ID.ToString().ToLower()})";
            }
        }

        public void UpdateChanges(Context context) {
            switch (this.Value) {
                case JObject jObject:
                    this.Controller.Update(jObject);
                    break;
                default:
                    throw new Exception("Invalid Value");
            }
        }
        public void UpdateChanges(Context context, JObject jObject) {
            foreach (JProperty jProperty in this.Value) {
                PropertyInfo propertyInfo = this.Controller.Model.GetType().GetProperty(jProperty.Name);
                if (propertyInfo != null) {
                    this.UpdateChanges(context, propertyInfo, jObject[jProperty.Name]);
                }
            }
        }
        public void UpdateChanges(Context context, PropertyInfo propertyInfo, JToken jToken) {
            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(Data.Model))) {
                Guid guid = new Guid();
                if (Guid.TryParse(jToken.ToString(), out guid)) {
                    Model bridge = context.Models.FirstOrDefault(x => x.ID == guid);
                    if (bridge?.Controller?.Model == null)
                        throw new Exception("");
                    propertyInfo.SetValue(this.Controller.Model, bridge.Controller.Model);
                    return;
                }
            }
            this.Controller.Update(propertyInfo, jToken);
        }

        public void ValidateChanges() {
            this.Message = this.Controller.Validate();
            Type modelType = this.Controller.Model.GetType();
            if (modelType.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                modelType = modelType.BaseType;
            this.Message.Name = $"{modelType.Name}({this.ID.ToString().ToLower()})";                
        }

        public void RefreshValue() {
            this.Value = this.Controller.Write();
        }

    }

}
