using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Requests.Context {
    public class Initilaize : STA.Data.Request {
        public Initilaize() : base() { }

        public override Response Execute() {
            this.Response = new Response(this);
            //LMS.Data.Bridge.Context bridgeContext = new LMS.Data.Bridge.Context();
            //this.Response.Result = bridgeContext.Models;
            return this.Response;
        }
    }
}
