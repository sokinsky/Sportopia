using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.sta.com.Controllers {
    public class ContextController : ApiController {
        [HttpPost]
        public STA.Data.Response Initialize(STA.Data.Requests.Context.Initilaize request) {
            return request.Execute();
        }
        [HttpPost]
        public STA.Data.Response SaveChanges(STA.Data.Requests.Context.SaveChanges request) {
            return request.Execute();
        }
    }
}
