using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Bridge {
    public class Context {
        public Context() { }
        public Context(List<Bridge.Model> models) {
            this.Models = models;
        }

        public Model this[Guid guid] {
            get {
                return this.Models.FirstOrDefault(x => x.ID == guid);
            }
        }
        public List<Model> Models { get; set; }
    }
}
