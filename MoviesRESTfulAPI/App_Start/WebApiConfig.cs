using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MoviesRESTfulAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            // Web API configuration and services
            
            //config.Formatters.Add(config.Formatters.XmlFormatter);
        }
    }
}
