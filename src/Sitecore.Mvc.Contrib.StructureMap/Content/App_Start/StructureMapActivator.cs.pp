
using System.Web.Mvc;

using Sitecore.Mvc.Contrib.StructureMap;
using StructureMap;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof($rootnamespace$.App_Start.StructureMapActivator), "PreStart")]

namespace $rootnamespace$.App_Start
{
    public static class StructureMapActivator
    {
        public static void PreStart() {

            IContainer container = new Container(x =>
            {
            });

            IoC.Configure(container);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}