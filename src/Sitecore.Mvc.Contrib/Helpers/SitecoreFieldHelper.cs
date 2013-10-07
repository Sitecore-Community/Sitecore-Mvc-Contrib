using System.Web;

using Sitecore.Mvc.Helpers;

namespace Sitecore.Mvc
{
    public static class SitecoreFieldHelper
    {
        public static HtmlString LinkField(this SitecoreHelper helper, string fieldName, string text,
                                           bool disableWebEdit = false)
        {
            return helper.Field(fieldName, new {text, DisableWebEdit = disableWebEdit});
        }
    }
}