using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA {
    public class Error {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Error> InnerErrors { get; set; }
    }
    public class Warning {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Warning> InnerWarnings { get; set; }
    }
    public class Exception  {
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception InnerException { get; set; }

        public Exception(System.Exception e) {
            this.Message = e.Message;
            if (e.InnerException != null) {
                this.InnerException = new Exception(e.InnerException);
            }
        }
    }
}
