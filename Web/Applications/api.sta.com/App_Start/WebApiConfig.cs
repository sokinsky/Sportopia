using System.Web.Http;
using System.Web.Http.Cors;

namespace api.sta.com {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{action}"
            );
            //config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
        }
    }
}
