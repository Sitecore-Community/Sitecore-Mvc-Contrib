using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Controllers;

namespace Website.Controllers
{
    public class HelloWorldController : Sitecore.Mvc.Contrib.Controllers.ControllerBase
    {
        // Override is necessary as SitecoreController provides an Index action method. Alernatively use the ActionNameAttribute
        public ActionResult Index()
        {
            ViewBag.Title = "Injected View in Placeholder";
            return View();
        }

        public ActionResult Redir()
        {
            return Redirect("/hello");
        }
    }
}
