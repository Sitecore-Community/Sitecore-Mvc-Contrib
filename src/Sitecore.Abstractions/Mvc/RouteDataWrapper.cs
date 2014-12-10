using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Routing;

namespace Sitecore.Mvc
{
    public class RouteDataWrapper : IRouteData
    {
        private readonly RouteData _routeData;

        public RouteDataWrapper(RouteData routeData)
        {
            _routeData = routeData;
        }



        public RouteValueDictionary Values
        {
            get { return _routeData.Values; }
        }

        public RouteValueDictionary DataTokens
        {
            get { return _routeData.DataTokens; }
        }
    }
}
