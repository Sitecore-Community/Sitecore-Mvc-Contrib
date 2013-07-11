using System.Web.Mvc;

using Ninject;

using Sitecore.Mvc.Contrib;
using Sitecore.Mvc.Contrib.Ninject;
using Sitecore.Mvc.Contrib.Reflection;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof($rootnamespace$.App_Start.NinjectActivator), "PreStart")]

namespace $rootnamespace$.App_Start
{
    public static class NinjectActivator
    {
        public static void PreStart() {

            var kernel = new StandardKernel();

            var assembly = ReflectionUtil.GetAssemblyFromName("$AssemblyName$");

			if (assembly != null)
			{
			    kernel.Load(assembly);
			}

            IoC.Configure(kernel);
 
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}