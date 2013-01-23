using System.Web.Mvc;
using Sitecore.Mvc.Controllers;

namespace Sitecore.Mvc.Contrib.Sample.Areas.SampleArea.Controllers
{
    public class SampleAreaController : SitecoreController
    {
        //
        // GET: /SampleArea/SampleArea/

        public override ActionResult Index()
        {
            // Dev. Note: Attempting to render a view using default search locations will fail currently.
            // The following locations are searched 
            //    ~/Views/SampleArea/Index.aspx
            //    ~/Views/SampleArea/Index.ascx
            //    ~/Views/Shared/Index.aspx
            //    ~/Views/Shared/Index.ascx
            //    ~/Views/SampleArea/Index.cshtml
            //    ~/Views/SampleArea/Index.vbhtml
            //    ~/Views/Shared/Index.cshtml
            //    ~/Views/Shared/Index.vbhtml 
            //
            // return View();

            // Support acknowledge that this is a issue. Workaround is to specify the view path.
            return View("~/Areas/SampleArea/Views/SampleArea/Index.cshtml");
        }
    }
}
