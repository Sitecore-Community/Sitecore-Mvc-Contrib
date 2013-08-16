using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class IsChildActionController : Controller
    {
        public ActionResult Index()
        {
            // Establish if this controller is exhibiting ChildAction behavior 
            ViewBag.IsChildAction = ControllerContext.IsChildAction;

            // .. and let's see what Renderer is in use
            var renderingContext = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;
            if (renderingContext != null)
                ViewBag.RenderingType = renderingContext.Rendering.RenderingType;

            return View();
        }
    }
}
