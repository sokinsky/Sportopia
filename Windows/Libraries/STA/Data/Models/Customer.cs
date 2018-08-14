using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Models {
    public enum CustomerStatus { Active, Inactive }
    [Table("Customers")]
    public class Customer  : Model {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int ID { get; set; }
        public virtual Person Person { get; set; }

        public CustomerStatus Status { get; set; }

    }
}
