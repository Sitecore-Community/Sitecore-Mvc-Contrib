using System.Web.Mvc;
using Sitecore.Mvc.Contrib.Helpers;

namespace Sitecore.Mvc.Contrib.Controllers
{
    public class StandardController : Controller
    {
        private readonly ISitecoreViewHelper _helper;
        public object Model { get; set; }

        public StandardController(ISitecoreViewHelper helper)
        {
            _helper = helper;
        }

        public virtual ActionResult DefaultAction(object model = null)
        {
            if (_helper.Disabled(RouteData))
            {
                return new EmptyResult();
            }

            return GetDefaultAction(model);
        }

        protected virtual ActionResult GetDefaultAction(object model = null)
        {
            var pageView = _helper.GetDefaultView();

            if (pageView == null)
            {
                return Redirect(_helper.ResolveRedirectUrl());
            }

            return model != null ? View(pageView, model) : View(pageView);
        }

    }
}
