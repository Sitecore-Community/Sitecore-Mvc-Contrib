using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Mvc.Contrib.Controllers;

namespace Sitecore.Mvc.Contrib.Extensions
{
    public static class ControllerRouteExtensions
    {
        public static Route MapControllerRoute(this RouteCollection routes, IController controller, string name, string url)
        {
            return MapControllerRoute(routes, controller, name, url, null /* defaults */, (object)null /* constraints */);
        }

        public static Route MapControllerRoute(this RouteCollection routes, IController controller, string name, string url, object defaults)
        {
            return MapControllerRoute(routes, controller, name, url, defaults, (object)null /* constraints */);
        }

        public static Route MapControllerRoute(this RouteCollection routes, IController controller, string name, string url, object defaults, object constraints)
        {
            return MapControllerRoute(routes, controller, name, url, defaults, constraints, null /* namespaces */);
        }

        public static Route MapControllerRoute(this RouteCollection routes, IController controller, string name, string url, string[] namespaces)
        {
            return MapControllerRoute(routes, controller, name, url, null /* defaults */, null /* constraints */, namespaces);
        }

        public static Route MapControllerRoute(this RouteCollection routes, IController controller, string name, string url, object defaults, string[] namespaces)
        {
            return MapControllerRoute(routes, controller, name, url, defaults, null /* constraints */, namespaces);
        }

        public static Route MapControllerRoute(this RouteCollection routes, IController controller, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if(controller == null)
            {
                throw new ArgumentNullException("controller");   
            }

            var route = new Route(url, new MvcRouteHandler())
            {
                Defaults = CreateRouteValueDictionary(defaults),
                Constraints = CreateRouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            route.Defaults.Add("controller", controller.GetType().Name.Replace("Controller",String.Empty));

            routes.Add(name, route);

            return route;
        }

        private static RouteValueDictionary CreateRouteValueDictionary(object values)
        {
            var dictionary = values as IDictionary<string, object>;

            if (dictionary != null)
            {
                return new RouteValueDictionary(dictionary);
            }

            return new RouteValueDictionary(values);
        }

        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}