using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Devices;

namespace Sitecore.Mvc.Presentation
{
    public interface IPageContext
    {
        Database Database { get; }
        Device Device { get; }
        HtmlHelper HtmlHelper { get; }
        Item Item { get; }
        PageDefinition PageDefinition { get; }
        IView PageView { get; }
        RequestContext RequestContext { get; }
    }
}