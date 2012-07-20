using System.Web.Mvc;
using System.Web.Routing;

namespace Sitecore.Mvc.Contrib.Helpers
{
    public interface ISitecoreViewHelper
    {
        IView GetDefaultView();
        string ResolveRedirectUrl();
        bool Disabled(RouteData routedata);
    }
}