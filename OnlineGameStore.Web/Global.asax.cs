using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineGameStore.DAL.DBContext;
using System.Data.Entity;
using OnlineGameStore.Web.AutoMapper;
using OnlineGameStore.BLL.AutoMapper;
using OnlineGameStore.Web.Logs;

namespace OnlineGameStore.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebConfiguration.Configure();
            AutoMapperBLLConfiguration.Configure();
            LoggingConfiguration.Configure();
            Database.SetInitializer(new EntitiesContextInitializer());
        }
    }
}
