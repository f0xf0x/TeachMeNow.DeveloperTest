using System.Web.Http;

using Newtonsoft.Json;

//using System.Web.Http.Cors;

namespace TeachMeNow.DeveloperTest.BackEnd {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            //TODO doesn't work at all
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {
                    id = RouteParameter.Optional
                }
            );
            var jsonSettings = new JsonSerializerSettings {
                // Note the ISO format
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DefaultValueHandling = DefaultValueHandling.Include,
                TypeNameHandling = TypeNameHandling.Auto
            };
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings = jsonSettings;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        }
    }
}