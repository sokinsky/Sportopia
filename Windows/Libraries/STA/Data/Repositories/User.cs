using LMS.Data;
using Newtonsoft.Json.Linq;
using STA.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Repositories {
    public class User : LMS.Data.Repository<Context, Models.User> {
        public User(Context context) : base(context) { }

        //public override IController<Context, Models.User> Create(JToken jToken) {
        //    Models.User user = new Models.User();

        //    var personController = this.Context["Person"].Select(jToken);            
        //    if (personController == null) {
        //        Models.Person person = new Models.Person();
        //        this.Context.People.Add(person);
        //        user.Person = person;
        //        person.User = user;
        //    }
        //    else {
        //        user.Person = (Models.Person)personController.Model;
        //        ((Models.Person)personController.Model).User = user;
        //    }
        //    //var dbset = this.Context.Set<Models.User>();
        //    //dbset.Attach(user);
        //    return this.createController(user);
        //}
        public override IController<Context, Models.User> Create(JObject jObject) {
            Models.User user = new Models.User();
            if (jObject.ContainsKey("ID")) {
                var personController = this.Context["Person", jObject["ID"]];
                if (personController != null) {
                    ((Models.Person)personController.Model).User = user;
                    return this.createController(user);
                }
            }
            if (jObject.ContainsKey("Person")) {
                var personController = this.Context["Person", jObject["Person"]];
                if (personController != null) {
                    ((Models.Person)personController.Model).User = user;
                    return this.createController(user);
                }
            }
            user.Person = new Models.Person();
            this.Context.Users.Add(user);
            return this.createController(user);
        }
    }
}
