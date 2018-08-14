using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA {
    public enum ValidationState { Insert, Update, Delete }
    public enum ValidationStatus { OK, Error, Warning }
    public class Validation {
        public ValidationState State { get; set; }
        public ValidationStatus Status { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public Dictionary<string, Validation> InnerValidations { get; set; }
    }
}
