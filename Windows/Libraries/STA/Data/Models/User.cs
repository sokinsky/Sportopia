using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.Models {
    [Table("Users")]
    public class User : Model {
        public User() {
            this.Token = Encryption.Encrypt(Guid.NewGuid().ToString());
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int ID { get; set; }
        public virtual Person Person { get; set; }

        public string Token { get; set; }
        [Index("IX_Username", IsUnique = true)]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
