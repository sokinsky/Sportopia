using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STA.Data.Validators {
    public class Phone : LMS.Data.Validator<Context, Models.Phone> {
        public Phone(LMS.Data.Controller<Context, Models.Phone> controller) : base(controller) { }

        public override LMS.Data.Message Validate(PropertyInfo propertyInfo) {
            switch (propertyInfo.Name) {
                case "Number":
                    if (this.Model.Number != null) {
                        if (!Regex.Match(this.Model.Number, @"^\d{10}$").Success)
                            return LMS.Data.Message.Invalid("Number", @"Invalid Format(^\d{10}$)");
                    }
                    break;
                default:
                    return base.Validate(propertyInfo);
            }
            return base.Validate(propertyInfo);
        }
    }
}
