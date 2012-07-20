using System;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Sitecore.Web;

namespace Sitecore.Mvc.Contrib.Helpers
{
    public class SitecoreViewHelper : ISitecoreViewHelper
    {
        public IView GetDefaultView()
        {
            var pageView = PageContext.Current.PageView;
            return pageView;
        }

        public string ResolveRedirectUrl()
        {
            var current = PageContext.Current;
            var str1 = current.RequestContext.HttpContext.Request.Url.LocalPath;
            var str2 = (current.RequestContext.RouteData.Route as Route).ValueOrDefault((Func<Route, string>)(route => route.Url));
            if (str2 != null)
            {
                str1 = str1 + Sitecore.StringExtensions.StringExtensions.FormatWith(" (route: {0})", new object[1] { (object) str2 });
            }
            var userName = Context.GetUserName();
            var siteName = Context.GetSiteName();
            return WebUtil.AddQueryString(Settings.ItemNotFoundUrl, "item", str1, "user", userName, "site", siteName);
        }

        public bool Disabled(RouteData routedata)
        {
            return (routedata.Values["scOutputGenerated"] as string).ToBool();
        }
    }
}