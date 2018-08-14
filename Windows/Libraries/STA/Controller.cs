using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data {
    public class Controller<TModel> : LMS.Data.Controller<Context, TModel> where TModel:Model {
        public Controller(Context context, TModel model) : base(context, model) { }
    }
}
