using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Requests.Model {
    public class Select : Request {
        public string Type { get; set; }
        public JToken Value { get; set; }

 
        public override Response Execute() {
            this.Response = new Response(this);
            return this.Response;
        }
    }
}
