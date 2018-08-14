using Newtonsoft.Json;
using STA.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;

namespace api.sta.com.Filters {
    public class Exceptions : ExceptionFilterAttribute {
        public override void OnException(HttpActionExecutedContext context) {
            STA.Data.Request request = (STA.Data.Request)context.ActionContext.ActionArguments.Values.SingleOrDefault(a => a is STA.Data.Request);
            request.Response.Result = null;
            if (context.Exception is DbEntityValidationException) {
                request.Response.Status.Type = STA.Data.ResponseStatusType.Exception;
                //request.Response.Status.Exception = STA.Exception(context.Exception);
            }
            else {
                request.Response.Status.Type = STA.Data.ResponseStatusType.Exception;
                //request.Response.Status.Exception = STA.Data.ExceptionError.FromException(context.Exception);
            }
            context.Response = new HttpResponseMessage();
            context.Response.Content = new StringContent(JsonConvert.SerializeObject(request.Response, Formatting.Indented));
        }
    }
}
