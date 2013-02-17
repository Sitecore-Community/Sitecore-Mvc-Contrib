using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using Sitecore.Mvc.Contrib;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof($rootnamespace$.App_Start.SitecoreMvcContrib.AutofacActivator), "PreStart")]

namespace $rootnamespace$.App_Start.SitecoreMvcContrib
{
    public static class AutofacActivator
    {
        public static void PreStart() {

            var builder = new ContainerBuilder();

            var assembly = ReflectionUtil.GetAssemblyFromName("$AssemblyName$");

			if (assembly != null)
			{
                builder.RegisterControllers(assembly);
			}

            IoC.Configure(builder);
 
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}