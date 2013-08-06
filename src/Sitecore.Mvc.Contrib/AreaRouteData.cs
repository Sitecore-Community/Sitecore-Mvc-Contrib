namespace Sitecore.Mvc.Contrib
{
    public class AreaRouteData
    {
        public AreaRouteData(string controller, string action, string area, bool useChildActionBehaviour)
        {
            Controller = controller;
            Action = action;
            Area = area;
            UseChildActionBehaviour = useChildActionBehaviour;
        }

        // By default UseChildActionBehaviour is false
        public AreaRouteData(string controller, string action, string area)
            : this(controller, action, area, false)
        {
        }

        public AreaRouteData()
        {
            
        }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public bool UseChildActionBehaviour { get; set; }
    }
}
