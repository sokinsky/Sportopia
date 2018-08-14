using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data {
    public enum ResponseStatusType { OK, Error, Warning, Exception }
    public class ResponseStatus {
        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseStatusType Type { get; set; }
        public int Code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;        
    }
    public class Response {
        public Response(Request request) {
            this.Request = request;
        }
        [JsonIgnore]
        public Request Request { get; set; }
        
        public ResponseStatus Status { get; set; } = new ResponseStatus {
            Type = ResponseStatusType.OK
        };
        public object Result { get; set; }
    }
}
