using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.sta.com.Controllers {
    public class UserController : ApiController {
        [HttpPost]
        public STA.Data.Models.Person Post(object person) {
            //using (STA.Data.Context context = new STA.Data.Context()) {
            //    context.People.Add(person);
            //    context.SaveChanges();
            //}
            //return person;
            return null;
        }
    }
}
