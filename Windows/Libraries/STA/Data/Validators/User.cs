using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Validators {
    public class User : LMS.Data.Validator<Context, Models.User> {
        public User(LMS.Data.Controller<Context, Models.User> controller) : base(controller) { }

        public override LMS.Data.Message Validate(PropertyInfo propertyInfo) {
            switch (propertyInfo.Name) {
                case "Username":
                    if (string.IsNullOrWhiteSpace(this.Model.Username))
                        return LMS.Data.Message.Invalid("Username", "Required");
                    break;
                case "Password":
                    if (string.IsNullOrWhiteSpace(this.Model.Password))
                        return LMS.Data.Message.Invalid("Password", "Required");
                    break;
            }
            return base.Validate(propertyInfo);
        }
    }
}
