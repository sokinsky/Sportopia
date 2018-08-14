using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using STA.Data.Models;

namespace STA.Data.Repositories {
    public class Phone : LMS.Data.Repository<Context, Models.Phone> {
        public Phone(Context context) : base(context) { }

        //public override LMS.Data.IController<Context, Models.Phone> Select(JValue jValue) {
        //    Models.Phone phone = null;
        //    switch (jValue.Value) {
        //        case string strValue:
        //            phone = this.Context.Phones.FirstOrDefault(x => x.Number == strValue);
        //            if (phone == null) {
        //                phone = new Models.Phone { Number = jValue.ToString() };
        //                this.Context.Phones.Add(phone);
        //            }
        //            return new Controller<Models.Phone>(this.Context, phone);
        //        default:
        //            var result = base.Select(jValue);
        //            if (result == null) {
        //                result = this.Create();
        //                result.Update(jValue);
        //            }
        //            return result;
        //    }
        //}
        //public override LMS.Data.IController<Context, Models.Phone> Select(JObject jObject) {
        //    if (jObject.ContainsKey("Number"))
        //        return this.Select(jObject["Number"]);
        //    return base.Select(jObject);
        //}
    }
}
