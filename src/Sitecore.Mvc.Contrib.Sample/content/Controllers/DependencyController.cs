using System.Web.Mvc;

using Sitecore.Mvc.Controllers;

using Website.Interfaces;

namespace Website.Controllers
{
    public class DependencyController : Sitecore.Mvc.Contrib.Controllers.ControllerBase
    {
        private readonly IFooService _fooService;

        public DependencyController(IFooService fooService)
        {
            _fooService = fooService;
        }

        public DependencyController() : this(new Website.Services.ConcreteFooService()) 
        {
            // Poor mans DI
            //
            // Register the construction logic for building an IFooService in the IoC.Configure method using your chosen DI container and 
            // then comment out parameterless constructor DependencyController and allow the DI container take care of creating these controller 
            // dependencies for you.
            //
            // Autofac is installed with the Sample project by default. NuGet packages have been provided for a range of other DI options 
            // including Ninject, Structure Map and Castle Windsor.
        }

        public ActionResult Index()
        {
            var model = _fooService.BusinessLogicCall();
            return View(model);
        }
    }
}

