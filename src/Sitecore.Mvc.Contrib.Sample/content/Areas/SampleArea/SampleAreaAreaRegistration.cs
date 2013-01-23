using System.Web.Mvc;

namespace Sitecore.Mvc.Contrib.Sample.Areas.SampleArea
{
    public class SampleAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SampleArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SampleArea_default",
                "SampleArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
