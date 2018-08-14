using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace STA.Data.Models {
    [Table("People")]
    public class Person : Model {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }

        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
