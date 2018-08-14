using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Json.Converters {
    public class GuidConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(Guid));
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            switch (reader.Value) {
                case string strGuid:
                    Guid result = new Guid();
                    if (Guid.TryParse(strGuid, out result))
                        return result;
                    break;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString().ToLower());
        }
    }
}
