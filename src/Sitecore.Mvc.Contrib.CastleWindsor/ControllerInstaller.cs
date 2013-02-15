using System.Reflection;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Sitecore.Mvc.Controllers;

namespace Sitecore.Mvc.Contrib.CastleWindsor
{
    /// <summary>
    /// Installs SitecoreController derived objects from a specified assembly
    /// </summary>
    public class ControllerInstaller : IWindsorInstaller
    {
        private readonly Assembly _controllerAssembly;

        public ControllerInstaller(Assembly controllerAssembly)
        {
            _controllerAssembly = controllerAssembly;
        }

        /// <summary>
        /// Installs the controllers
        /// </summary>
        /// <param name="container"></param>
        /// <param name="store"></param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(FindControllers().Configure(x => x.LifestyleTransient()));
        }

        /// <summary>
        /// Find controllers within this assembly
        /// </summary>
        /// <returns></returns>
        private BasedOnDescriptor FindControllers()
        {
            return AllTypes.FromAssembly(_controllerAssembly).BasedOn<SitecoreController>();
        }
    }
}