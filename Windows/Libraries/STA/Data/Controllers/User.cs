using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace STA.Data.Controllers {
    public class User : LMS.Data.Controller<Context, Models.User> {
        public User(Context context, Models.User model) : base(context, model) { }
        //public override void Update(JObject jObject) {
        //    if (jObject.ContainsKey("ID")) {
        //        this.Model.ID = this.Context["Person", jObject["ID"]].Model.ID;                
        //    }
        //    else {
        //        using (STA.Data.Context context = new STA.Data.Context()) {
        //            var personController = context["Person"].Create();
        //            personController.Update(jObject);
        //            context.SaveChanges();

        //            this.Model.ID = personController.Model.ID;
        //        }                    
        //    }
        //    base.Update(jObject);
        //}
    }
}
