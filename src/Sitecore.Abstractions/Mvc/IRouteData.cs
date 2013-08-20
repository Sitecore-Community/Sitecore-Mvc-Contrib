using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Sitecore.Mvc
{
    public interface IRouteData
    {
        RouteValueDictionary Values { get; }
        RouteValueDictionary DataTokens { get; }
    }
}
