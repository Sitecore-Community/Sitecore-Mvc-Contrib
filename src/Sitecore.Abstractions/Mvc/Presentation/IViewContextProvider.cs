using System.Web.Mvc;

namespace Sitecore.Mvc.Presentation
{
    public interface IViewContextProvider
    {
        ViewContext GetCurrentViewContext();
    }
}