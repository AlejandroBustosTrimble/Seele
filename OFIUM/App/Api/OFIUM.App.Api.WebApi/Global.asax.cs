using OFIUM.App.Api.DIRegistration;
using RAK.Core.Api.Model.Interfaces;
using RAK.Core.Api.WebApi;
using RAK.Fwk.Api.Adapter;
using RAK.Fwk.Api.Model;
using RAK.Fwk.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OFIUM.App.Api.WebApi
{
    public class WebApiApplication : WebApiApplicationBase
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var diRegis = new DIRegistrator();
            diRegis.Register();
        }
    }
}
