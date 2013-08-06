using Sitecore.Mvc.Common;
using System.Web.Mvc;

namespace Sitecore.Mvc.Presentation
{
    public class ViewContextProvider :IViewContextProvider
    {
        public ViewContext GetCurrentViewContext()
        {
            return ContextService.Get().GetCurrent<ViewContext>();
        }
    }
}
