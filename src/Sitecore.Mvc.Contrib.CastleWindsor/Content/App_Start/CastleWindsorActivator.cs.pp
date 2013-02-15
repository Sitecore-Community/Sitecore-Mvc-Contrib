using System;
using System.Linq;
using System.Reflection;

using Castle.Windsor;

using Sitecore.Mvc.Contrib.CastleWindsor;

using ControllerBuilder = System.Web.Mvc.ControllerBuilder;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof($rootnamespace$.App_Start.SitecoreMvcContrib.CastleWindsorActivator), "PreStart")]

namespace $rootnamespace$.App_Start.SitecoreMvcContrib
{
    public static class CastleWindsorActivator
    {
        public static void PreStart() {

            var container = new WindsorContainer();

            var assembly = GetAssemblyFromName("$AssemblyName$");

			if (assembly != null)
			{
				container.Install(new ControllerInstaller(assembly));
			}

            IoC.Configure(container);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }

        private static Assembly GetAssemblyFromName(string assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.GetName().Name == assemblyName);
        }
    }
}