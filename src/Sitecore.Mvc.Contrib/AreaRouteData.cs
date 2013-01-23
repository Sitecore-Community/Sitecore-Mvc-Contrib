namespace Sitecore.Mvc.Contrib
{
    public class AreaRouteData
    {
        public AreaRouteData(string controller, string action, string area)
        {
            Controller = controller;
            Action = action;
            Area = area;
        }

        public AreaRouteData()
        {
            
        }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
    }
}
