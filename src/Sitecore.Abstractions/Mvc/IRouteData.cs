using System.Web.Routing;

namespace Sitecore.Mvc
{
    public interface IRouteData
    {
        RouteValueDictionary Values { get; }
        RouteValueDictionary DataTokens { get; }
    }
}