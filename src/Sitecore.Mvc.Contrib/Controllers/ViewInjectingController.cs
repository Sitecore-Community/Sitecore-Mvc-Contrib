using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Configuration;
using Sitecore.Mvc.Common;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;
using Sitecore.Web;

namespace Sitecore.Mvc.Contrib.Controllers
{
    public class ViewInjectingController : Controller
    {
        public ViewInjectingController()
        {
            Placeholder = "main";
        }

        public string Placeholder { get; set; }

        protected IView InjectViewInPlace(string placeholder, IView view, object model)
        {
            if (placeholder != null)
            {
                var contentRendering = new ContentRendering()
                {
                    Id = new Guid(),
                    UniqueId = new Guid(),
                    Placeholder = placeholder,
                    DeviceId = PageContext.Current.Device.Id
                };

                var sw = new StringWriter();
                if (model != null) ViewData.Model = model;

                var viewConext = new ViewContext(this.ControllerContext, view, ViewData, TempData, sw);
                view.Render(viewConext, sw);
                contentRendering.Content = sw.ToString();

                var currentRenderings = PageContext.Current.PageDefinition.Renderings;
                currentRenderings.Add(contentRendering);
            }

            return PageContext.Current.PageView;
        }

        protected virtual ActionResult RedirectToNotFound()
        {
            PageContext current = PageContext.Current;
            string localPath = current.RequestContext.HttpContext.Request.Url.LocalPath;
            string str2 = (current.RequestContext.RouteData.Route as Route).ValueOrDefault<Route, string>(route => route.Url);
            if (str2 != null)
            {
                localPath = localPath + " (route: {0})".FormatWith(new object[] { str2 });
            }
            string userName = Context.GetUserName();
            string siteName = Context.GetSiteName();
            string url = WebUtil.AddQueryString(Settings.ItemNotFoundUrl, new string[] { "item", localPath, "user", userName, "site", siteName });
            return this.Redirect(url);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            var controllerContext = this.ControllerContext;

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controllerContext.RouteData.GetRequiredString("action");
            }

            var placeHolder = controllerContext.RouteData.GetRequiredString("scPlaceholder");

            if(string.IsNullOrEmpty(placeHolder))
            {
                placeHolder = Placeholder;
            }

            ViewEngineResult result = ViewEngines.Engines.FindView(controllerContext, viewName, masterName);
            var pageView = InjectViewInPlace(placeHolder, result.View, model);
            result.ViewEngine.ReleaseView(controllerContext, result.View);

            return base.View(pageView, model);
        }

        protected override ViewResult View(IView view, object model)
        {
            var placeHolder = ControllerContext.RouteData.GetRequiredString("scPlaceholder");

            if (string.IsNullOrEmpty(placeHolder))
            {
                placeHolder = Placeholder;
            }

            var pageView = InjectViewInPlace(placeHolder, view, model);
            return base.View(pageView, model);
        }
    }
}
