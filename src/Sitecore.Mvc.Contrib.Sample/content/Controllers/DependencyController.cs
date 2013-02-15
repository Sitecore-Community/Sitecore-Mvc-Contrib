using System.Web.Mvc;

using Sitecore.Mvc.Controllers;

using Website.Interfaces;

namespace Website.Controllers
{
    public class DependencyController : SitecoreController
    {
        private readonly IFooService _fooService;

        public DependencyController(IFooService fooService)
        {
            _fooService = fooService;
        }

        public override ActionResult Index()
        {
            var model = _fooService.BusinessLogicCall();
            return View(model);
        }
    }
}

