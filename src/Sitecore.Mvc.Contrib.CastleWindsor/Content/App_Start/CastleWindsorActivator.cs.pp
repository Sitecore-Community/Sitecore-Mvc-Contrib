using Castle.Windsor;

using Sitecore.Mvc.Contrib;
using Sitecore.Mvc.Contrib.CastleWindsor;

using ControllerBuilder = System.Web.Mvc.ControllerBuilder;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof($rootnamespace$.App_Start.SitecoreMvcContrib.CastleWindsorActivator), "PreStart")]

namespace $rootnamespace$.App_Start.SitecoreMvcContrib
{
    /// <remarks>
    /// It seems that a traditional controller factory implementation is the best approach for Windsor and MVC3.
    /// Ref. http://stackoverflow.com/questions/4140860/castle-windsor-dependency-resolver-for-mvc-3
    /// </remarks>>
    public static class CastleWindsorActivator
    {
        public static void PreStart() {

            var container = new WindsorContainer();

            var assembly = ReflectionUtil.GetAssemblyFromName("$AssemblyName$");

			if (assembly != null)
			{
				container.Install(new ControllerInstaller(assembly));
			}

            IoC.Configure(container);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }
    }
}