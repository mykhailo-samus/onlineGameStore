﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;
namespace OnlineGameStore.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
