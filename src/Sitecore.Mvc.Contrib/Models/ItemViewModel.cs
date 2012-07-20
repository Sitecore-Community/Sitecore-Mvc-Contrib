using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Contrib.Helpers;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Mvc.Contrib.Models
{
    public class ItemViewModel : Sitecore.Mvc.Presentation.IRenderingModel
    {
        private HtmlHelper _htmlHelper;
        protected SitecoreHelper SitecoreMainHelper { get; private set; }

        public virtual void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            var current = Sitecore.Mvc.Common.ContextService.Get().GetCurrent<ViewContext>();
            _htmlHelper = new HtmlHelper(current, new Sitecore.Mvc.Presentation.ViewDataContainer(current.ViewData));
            SitecoreMainHelper = Sitecore.Mvc.HtmlHelperExtensions.Sitecore(_htmlHelper);
        }

        public HtmlString Created
        {
            get
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Sitecore.Context.Language.Name);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Sitecore.Context.Language.Name);
                var parameters = new Sitecore.Collections.SafeDictionary<string>();
                parameters["format"] = "D";
                return SitecoreMainHelper.RenderField(Sitecore.FieldIDs.Created.ToString(), false, parameters);
            }
        }
    }
}