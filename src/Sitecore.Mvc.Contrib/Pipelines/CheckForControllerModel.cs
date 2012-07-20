using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Pipelines
{
    public class CheckForControllerModel : GetModelProcessor
    {
        public override void Process(GetModelArgs args)
        {
            var currentViewContext = Sitecore.Mvc.Common.ContextService.Get().GetCurrent<System.Web.Mvc.ViewContext>();

            if (currentViewContext.ViewData.Model == null) return;

            if (currentViewContext.ViewData.Model is IRenderingModel)
            {
                args.Result = currentViewContext.ViewData.Model;
            }
        }
    }
}