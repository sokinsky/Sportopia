using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LMS.Data;

namespace STA.Data.Validators {
    public class Person : LMS.Data.Validator<Context, Models.Person> {
        public Person(LMS.Data.Controller<Context, Models.Person> controller) : base(controller) { }

        public override LMS.Data.Message Validate(PropertyInfo propertyInfo) {
            switch (propertyInfo.Name) {
                case "FirstName":
                    if (string.IsNullOrWhiteSpace(this.Model.FirstName))
                        return LMS.Data.Message.Invalid("Required");
                    break;
                case "LastName":
                    if (string.IsNullOrWhiteSpace(this.Model.LastName))
                        return LMS.Data.Message.Invalid("Reuqired");
                    break;
                default:
                    return base.Validate(propertyInfo);
            }
            return base.Validate(propertyInfo);
        }
    }
}
