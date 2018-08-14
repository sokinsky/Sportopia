using Microsoft.CSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using STA.Data;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace STA {
    public static class Extensions {
        public static T ByID<T>(this IEnumerable<T> enumerable, int id) where T : Model {
            return enumerable.FirstOrDefault(x => x.ID == id);
        }

        public static T Select<T>(this IEnumerable<T> enumerable, JToken jToken) where T:Model {
            if (jToken == null)
                return null;
            switch (jToken.GetType().Name) {
                case "JValue":
                    return enumerable.Select((JValue)jToken);
                case "JObject":
                    return enumerable.Select((JObject)jToken);
                default:
                    return null;
            }
        }
        public static T Select<T>(this IEnumerable<T> enumerable, JValue jValue) where T:Model {
            MethodInfo selectMethod = typeof(T).GetMethod("Select", new Type[] { jValue.GetType() });
            if (selectMethod != null)
                return (T)selectMethod.Invoke(null, new object[] { jValue });

            int id = 0;
            if (int.TryParse(jValue.Value.ToString(), out id)) 
                return enumerable.ByID(id);
            return default(T);
        }
        public static T Select<T>(this IEnumerable<T> enumerable, JObject jObject) where T:Model {
            MethodInfo selectMethod = typeof(T).GetMethod("Select", new Type[] { jObject.GetType() });
            if (selectMethod != null)
                return (T)selectMethod.Invoke(null, new object[] { jObject });

            if (jObject.ContainsKey("ID"))
                return enumerable.Select(jObject["ID"]);
            return null;
        }

        public static bool Contains(this Type type, Type of) {
            if (type == of || type.IsSubclassOf(of))
                return true;
            if (!type.IsGenericType) {
                foreach (Type genericType in type.GenericTypeArguments) {
                    if (genericType.Contains(of))
                        return true;
                }
            }
            return false;
        }
    }
}
