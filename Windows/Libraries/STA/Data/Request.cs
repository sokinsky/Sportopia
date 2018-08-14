using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace STA.Data {
    public class Request {
        internal Context _context;
        [JsonIgnore]
        public Context Context {
            get {
                if (this._context == null)
                    this._context = new Context();
                return this._context;
            }
        }
        [JsonIgnore]
        public Response Response { get; set; }

        public virtual Response Execute() {
            return new Response(this);
        }
    }
}
