using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Core;

namespace Sitecore.Mvc.Contrib.Autofac
{
    /// <summary>
    /// An MS-MVC controller factory that returns controllers built by an
    /// Autofac IoC container scoped according to the current request.
    /// </summary>
    public class AutofacControllerFactory : DefaultControllerFactory
    {
        readonly IContainerProvider _containerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacControllerFactory"/> class.
        /// </summary>
        /// <param name="containerProvider">The container provider.</param>
        public AutofacControllerFactory(IContainerProvider containerProvider)
        {
            if (containerProvider == null)
                throw new ArgumentNullException("containerProvider");

            _containerProvider = containerProvider;
        }

        // Enables testing
        internal IController CreateControllerInternal(RequestContext context, Type controllerType)
        {
            return GetControllerInstance(context, controllerType);
        }

        /// <summary>
        /// Creates the controller.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>The controller.</returns>
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // a null controller type is a 404, because the base class couldn't resolve the controller name back to a type
            // a common example of this case would be a non-existant favicon.ico
            if (controllerType == null)
            {
                throw new HttpException(404,
                    string.Format("controller type was not found for path {0}",
                        context.HttpContext.Request.Path));
            }

            var controllerService = new TypedService(controllerType);

            object controller;
            if (_containerProvider.RequestLifetime.TryResolveService(controllerService, out controller))
                return (IController)controller;

            throw new HttpException(404,
                string.Format(AutofacControllerFactoryResources.NotFound,
                    controllerService,
                    controllerType.FullName,
                    context.HttpContext.Request.Path));
        }


        /// <summary>
        /// Releases the controller. Unecessary in an Autofac-managed application
        /// </summary>
        /// <param name="controller">The controller.</param>
        public override void ReleaseController(IController controller)
        {
        }
    }
}
