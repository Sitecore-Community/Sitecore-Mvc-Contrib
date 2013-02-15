using System.Web.Mvc;
using System.Web.Routing;

using Sitecore.Diagnostics;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(Sitecore.Mvc.Contrib.Sample.App_Start.MvcContribPackage), "PreStart")]

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof(Sitecore.Mvc.Contrib.Sample.App_Start.MvcContribPackage), "PostStart")]

namespace Sitecore.Mvc.Contrib.Sample.App_Start
{
    public static class MvcContribPackage
    {
        private static ILog _log;

        public static ILog Log
        {
            get { return _log ?? (_log = new LogWrapper()); }

            set { _log = value; }
        }

        public static void PreStart()
        {
            Log.Info("MvcContribPackage: Performing PreStart actions", "MvcContribPackage");
            RegisterRoutes(RouteTable.Routes);
        }

        public static void PostStart()
        {
            Log.Info("MvcContribPackage: Performing PostStart actions", "MvcContribPackage");
            AreaRegistration.RegisterAllAreas();
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "HelloWorld", // Route name
                "hello/{action}/{id}", // URL with parameters
                new
                {
                    controller = "HelloWorld",
                    scItemPath = "/sitecore/content/Mvc Sample",
                    scPlaceholder = "main",
                    partialView = "true",
                    id = UrlParameter.Optional
                });
        }
    }
}