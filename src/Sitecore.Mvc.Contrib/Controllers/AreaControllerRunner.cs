using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Controllers
{
    public class AreaControllerRunner : ControllerRunner, IControllerRunner
    {
        private readonly IPageContext _pageContext;
        private readonly IRouteData _routeData;
        private readonly IViewContextProvider _viewContextProvider;

        public AreaControllerRunner(IPageContext pageContext, IRouteData routeData, IViewContextProvider viewContextProvider, AreaRouteData areaRouteData)
            : base(areaRouteData.Controller, areaRouteData.Action)
        {
            _pageContext = pageContext;
            _routeData = routeData;
            _viewContextProvider = viewContextProvider;
            Area = areaRouteData.Area;
            UseChildActionBehavior = areaRouteData.UseChildActionBehavior;
        }

        public AreaControllerRunner(AreaRouteData areaRouteData) : this(
            new PageContextWrapper(PageContext.Current), 
            new RouteDataWrapper(PageContext.Current.RequestContext.RouteData), 
            new ViewContextProvider(),
            areaRouteData)
        {

        }

        public string Area { get; set; }
        public bool UseChildActionBehavior { get; set; }

        protected override void ExecuteController(Controller controller)
        {
            object controllerValue = _routeData.Values["controller"];
            object actionValue = _routeData.Values["action"];
            object areaValue = _routeData.DataTokens["area"];
            object parentActionViewContext = _routeData.DataTokens["ParentActionViewContext"];
        
            try
            {
                _routeData.Values["controller"] = ActualControllerName;
                _routeData.Values["action"] = ActionName;
                _routeData.DataTokens["area"] = Area;

                if (UseChildActionBehavior)
                {
                    _routeData.DataTokens["ParentActionViewContext"] = _viewContextProvider.GetCurrentViewContext();
                }

                ((IController)controller).Execute(_pageContext.RequestContext);

            }
            finally
            {
                _routeData.Values["controller"] = controllerValue;
                _routeData.Values["action"] = actionValue;

                // Remove area data token if not available previously to resolve null exception from App Center's TagInjectionControllerFactory's CanHandle method.
                if (areaValue == null)
                    _routeData.DataTokens.Remove("area");
                else
                    _routeData.DataTokens["area"] = areaValue;

                if (UseChildActionBehavior)
                {
                    if (parentActionViewContext == null)
                        _routeData.DataTokens.Remove("ParentActionViewContext");
                    else
                        _routeData.DataTokens["ParentActionViewContext"] = parentActionViewContext;
                }
            }
        }
    }
}
