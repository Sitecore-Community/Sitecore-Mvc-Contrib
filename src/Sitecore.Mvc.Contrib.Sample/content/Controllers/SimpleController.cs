using System;
using System.Web.Mvc;

using Sitecore.Mvc.Contrib.Sample.Model;
using Sitecore.Mvc.Controllers;

namespace Website.Controllers
{
    public class SimpleController : SitecoreController
    {
        public override ActionResult Index()
        {
            var model = BusinessLogicCall();
            return View(model);
        }

        private SimpleViewModel BusinessLogicCall()
        {
            return new SimpleViewModel
                       {
                           Message = "Method was executed at: " + DateTime.Now.ToLongTimeString()
                       };
        }
    }
}
