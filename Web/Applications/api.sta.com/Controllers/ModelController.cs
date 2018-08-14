using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.sta.com.Controllers {
    public class ModelController : ApiController {
        [HttpPost]
        public STA.Data.Response Select(STA.Data.Requests.Model.Select request) {
            STA.Data.Response response = new STA.Data.Response(request);
            response.Result = request.Context[request.Type, request.Value]?.Write();
            return response;
        }

        [HttpGet]
        public System.Web.Http.IHttpActionResult ByID(string type, int id) {
            using (STA.Data.Context context = new STA.Data.Context()) {
                var model = context[type, id];
                if (model == null)
                    return NotFound();
                return Ok(model.Model);
            }

        }
    }
}
