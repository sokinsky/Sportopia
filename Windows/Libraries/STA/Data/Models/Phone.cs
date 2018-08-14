using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STA.Data.Models {
    [Table("Phones")]
    public class Phone : Model {
        [Required][MaxLength(11)]
        [Index("IX_Number", IsUnique = true)]
        public string Number { get; set; }

        //public override Error Validate(Context context, PropertyInfo propertyInfo) {
        //    switch (propertyInfo.Name) {
        //        case "Number":
        //            if (string.IsNullOrWhiteSpace(this.Number))
        //                return new Error { Message = "Required" };
        //            else if (!Regex.Match(this.Number, @"^\d{10}$").Success)
        //                return new Error { Message = "Invalid", Description = "Number is not a valid 10 digit number" };
        //            else {
        //                Phone duplicate = context.Phones.FirstOrDefault(x => x.Number == this.Number && x.ID != this.ID);
        //                if (duplicate != null)
        //                    return new Error { Message = "Duplicate", Description = $"Number({this.Number}) is already in the database" };
        //            }
        //            return null;
        //        default:
        //            return base.Validate(context, propertyInfo);
        //    }                        
        //}
        //public override Error Authroize(Context context, PropertyInfo propertyInfo) {
        //    switch (propertyInfo.Name) {
        //        case "Number":
        //            switch (this.CurrentState(context, propertyInfo)) {
        //                case EntityState.Modified:
        //                    return new Error { Message = "Unauthorized", Description = "You are unauthorized to change a number of an existing phone" };
        //            }
        //            return null;
        //        default:
        //            return base.Authroize(context, propertyInfo);
        //    }
            
        //}

        //public bool Primary { get; set; }

        //public static Phone Select(Context context, JToken jToken) {
        //    switch (jToken.GetType().Name) {
        //        case "JValue": return Select(context, (JValue)jToken);
        //        case "JObject": return Select(context, (JObject)jToken);
        //        default: return null;
        //    }
        //}
        //public static Phone Select(Context context, JValue jValue) {
        //    switch (jValue.Value.GetType().FullName) {
        //        case "System.Int64":
        //            return context.Phones.FirstOrDefault(x => x.ID == int.Parse(jValue.Value.ToString()));
        //        case "System.String":
        //            string strValue = jValue.Value.ToString();
        //            Phone result = context.Phones.FirstOrDefault(x => x.Number == strValue);
        //            if (result == null) {
        //                result = new Phone { Number = strValue };
        //                context.Phones.Add(result);
        //            }
        //            return result;
        //    }
        //    return null;
        //}
        //public static Phone Select(Context context, JObject jObject) {
        //    if (jObject.ContainsKey("Number"))
        //        return Select(context, jObject["Number"]);
        //    else if (jObject.ContainsKey("ID"))
        //        return Select(context, jObject["ID"]);
        //    return null;
        //}
    }
}
