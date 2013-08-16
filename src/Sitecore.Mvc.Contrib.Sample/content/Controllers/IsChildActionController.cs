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
            var renderingContext = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;

            if (renderingContext != null)
            {
                // Let's have a look at the general rendering details ...
                ViewBag.RenderingDetail = renderingContext.Rendering.ToString();

                // ... and check the type of the renderer being used.
                ViewBag.RendererType = renderingContext.Rendering.Renderer.GetType().ToString();
            }

            // And finally we need to establish if this controller is 
            // exhibiting ChildAction behavior.
            ViewBag.IsChildAction = ControllerContext.IsChildAction;

            return View();
        }
    }
}
