using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TaxiAutomation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "NewRoute",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.EnableSystemDiagnosticsTracing();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
