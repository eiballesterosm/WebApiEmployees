using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiEmployees
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Response in JSON
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("text/html")
            );

            //CORS
            config.EnableCors(new EnableCorsAttribute(ConfigurationManager.AppSettings["urlEnableCORS"], headers: "*", methods: "*"));
            //var cors = new EnableCorsAttribute(ConfigurationManager.AppSettings["urlEnableCORS"], "*", "*");
            //config.EnableCors(cors);
        }
    }
}
