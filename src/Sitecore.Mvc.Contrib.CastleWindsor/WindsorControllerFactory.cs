using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;

namespace Sitecore.Mvc.Contrib.CastleWindsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kernel"></param>
        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Release the controller at the end of it's life cycle
        /// </summary>
        /// <param name="controller">The Interface to an MVC controller</param>
        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        /// <summary>
        /// Resolve a controller dependency
        /// </summary>
        /// <param name="requestContext">The HTTP context</param>
        /// <param name="controllerType">Type of controller to resolve</param>
        /// <returns>IController</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.",
                    requestContext.HttpContext.Request.Path));
            }
            return (IController)_kernel.Resolve(controllerType);
        }
    }
}
