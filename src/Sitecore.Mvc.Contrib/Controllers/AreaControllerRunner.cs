using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Controllers
{
    public class AreaControllerRunner : ControllerRunner, IControllerRunner
    {
        private readonly IPageContext _pageContext;

        public AreaControllerRunner(IPageContext pageContext, AreaRouteData areaRouteData)
            : base(areaRouteData.Controller, areaRouteData.Action)
        {
            _pageContext = pageContext;
            Area = areaRouteData.Area;
        }

        public AreaControllerRunner(AreaRouteData areaRouteData) 
            : this(new PageContextWrapper(PageContext.Current), areaRouteData)
        {
        }

        public string Area { get; set; }
        
        protected override void ExecuteController(Controller controller)
        {
            RequestContext requestContext = _pageContext.RequestContext;
            object value = requestContext.RouteData.Values["controller"];
            object value2 = requestContext.RouteData.Values["action"];
            object value3 = requestContext.RouteData.DataTokens["area"];
            
            try
            {
                requestContext.RouteData.Values["controller"] = ActualControllerName;
                requestContext.RouteData.Values["action"] = ActionName;
                requestContext.RouteData.DataTokens["area"] = Area;

                ((IController)controller).Execute(_pageContext.RequestContext);
            }
            finally
            {
                requestContext.RouteData.Values["controller"] = value;
                requestContext.RouteData.Values["action"] = value2;
                requestContext.RouteData.DataTokens["area"] = value3;
            }
        }
    }
}
