using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Typescripting {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor)]
    public class Attribute : System.Attribute {
        public Attribute() {
            this.Enabled = true;
        }
        public Attribute(bool enabled) {
            this.Enabled = enabled;
        }
        public bool Enabled { get; set; }
    }
}
