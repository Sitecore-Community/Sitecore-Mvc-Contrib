using Sitecore.Mvc.Common;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sitecore.Mvc.Contrib.Presentation.Renderer
{
    public class ChildActionRenderer : Mvc.Presentation.ControllerRenderer
    {
        private readonly IPageContext _pageContext;
        private readonly HtmlHelper _helper;

        public ChildActionRenderer()
            : this(new PageContextWrapper(PageContext.Current), GetHtmlHelper())
        {
        }
 
        public ChildActionRenderer(IPageContext pageContext, HtmlHelper helper)
        {
            _pageContext = pageContext;
            _helper = helper;
        }
 
        public override void Render(System.IO.TextWriter writer)
        {
            string controllerName = this.ControllerName;
            string actionName = this.ActionName;
            if (!controllerName.IsWhiteSpaceOrNull() && !actionName.IsWhiteSpaceOrNull())
            {
                var value = RunChildAction(actionName, controllerName);
 
                if (value == null)
                {
                    return;
                }
                writer.Write(value);
            }
        }
 
        private MvcHtmlString RunChildAction(string actionName, string controllerName)
        {
            // Important note:
            // The Action extension method requires that a route exists in the route table
            // that maps the controller and action. This is standard child action behaviour.

            return _helper.Action(actionName, controllerName, _pageContext.RequestContext.RouteData.Values);
        }
 
        protected static HtmlHelper GetHtmlHelper()
        {
            ViewContext current = ContextService.Get().GetCurrent<ViewContext>();
            return new HtmlHelper(current, new ViewDataContainer(current.ViewData));
        }
    }
}