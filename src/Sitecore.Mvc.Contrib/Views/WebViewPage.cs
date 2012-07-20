namespace Sitecore.Mvc.Contrib.Views
{
    public abstract class WebViewPage : System.Web.Mvc.WebViewPage
    {
        public Sitecore.Mvc.Helpers.SitecoreHelper SitecoreHelper { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            this.SitecoreHelper = this.Html.Sitecore();
        }
    }

    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public Sitecore.Mvc.Helpers.SitecoreHelper SitecoreHelper { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            this.SitecoreHelper = this.Html.Sitecore();
        }
    }
}