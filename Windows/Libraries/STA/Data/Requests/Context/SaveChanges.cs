using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Requests.Context {
    public class SaveChanges : STA.Data.Request {
        public List<LMS.Data.Bridge.Model> Models{ get; set; }

        public override Response Execute() {
            this.Response = new Response(this);
            //this.Context.People.Add(new Data.Models.Person {
            //    FirstName = "Gordon",
            //    LastName = "Deap"
            //});
            //this.Response.Result = this.Context.SaveChanges();


            LMS.Data.Bridge.Context clientContext = new LMS.Data.Bridge.Context(this.Models);
            if (!this.Context.SaveChanges(clientContext)) {
                this.Response.Status.Type = ResponseStatusType.Error;
                this.Response.Status.Message = "Unable to save request";
            }
            else {
                this.Response.Status.Type = ResponseStatusType.OK;
                this.Response.Status.Message = "Changes Saved";
            }                
            this.Response.Result = clientContext.Models;
            return this.Response;

        }
    }

}
