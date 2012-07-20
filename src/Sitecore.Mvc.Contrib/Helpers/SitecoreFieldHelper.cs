using System.Globalization;
using System.Threading;
using System.Web;

namespace Sitecore.Mvc.Contrib.Helpers
{
    public static class SitecoreFieldHelper
    {
        public static HtmlString RenderField(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldNameOrId, bool disableWebEdit = false, Sitecore.Collections.SafeDictionary<string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Sitecore.Collections.SafeDictionary<string>();
            }

            return sitecoreHelper.Field(fieldNameOrId,
              new
                {
                    DisableWebEdit = disableWebEdit,
                    Parameters = parameters
                });
        }

        public static HtmlString RenderField(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, Sitecore.Data.ID fieldId, bool disableWebEdit = false, Sitecore.Collections.SafeDictionary<string> parameters = null)
        {
            return RenderField(sitecoreHelper, fieldId.ToString(), disableWebEdit, parameters);
        }

        public static HtmlString RenderDate(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldNameOrId, string format = "D", bool disableWebEdit = false, bool setCulture = true, Sitecore.Collections.SafeDictionary<string> parameters = null)
        {
            if (setCulture)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Sitecore.Context.Language.Name);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Sitecore.Context.Language.Name);
            }

            if (parameters == null)
            {
                parameters = new Sitecore.Collections.SafeDictionary<string>();
            }

            parameters["format"] = format;
            return RenderField(sitecoreHelper, fieldNameOrId, disableWebEdit, parameters);
        }

        public static HtmlString RenderDate(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, Sitecore.Data.ID fieldId, string format = "D", bool disableWebEdit = false, bool setCulture = true, Sitecore.Collections.SafeDictionary<string> parameters = null)
        {
            return RenderDate(sitecoreHelper, fieldId.ToString(), format, disableWebEdit, setCulture, parameters);
        }

        public static HtmlString TagField(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldNameOrId, string htmlElement, bool disableWebEdit = false, Sitecore.Collections.SafeDictionary<string> parameters = null)
        {
            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.ContextItem;

            if (item == null || string.IsNullOrEmpty(item[fieldNameOrId]))
            {
                return new HtmlString(string.Empty);
            }

            var value = sitecoreHelper.RenderField(fieldNameOrId, disableWebEdit, parameters).ToString();
            return new HtmlString(string.Format("<{0}>{1}</{0}>", htmlElement, value));
        }

        public static HtmlString TagField(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper,Sitecore.Data.ID fieldId,string htmlElement,bool disableWebEdit = false,Sitecore.Collections.SafeDictionary<string> parameters = null)
        {
            return TagField(sitecoreHelper, fieldId.ToString(), htmlElement, disableWebEdit, parameters);
        }
    }
}