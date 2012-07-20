using Sitecore.Mvc.Contrib.Controllers;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Pipelines
{
    public class SetModelFromController : GetModelProcessor
    {
        public override void Process(GetModelArgs args)
        {
            var currentViewContext = Sitecore.Mvc.Common.ContextService.Get().GetCurrent<System.Web.Mvc.ViewContext>();

            if (currentViewContext.ViewData.Model == null) return;

            if (!(currentViewContext.Controller is StandardController)) return;

            var controller = currentViewContext.Controller as StandardController;

            if (controller.Model == null || !(controller.Model is IRenderingModel)) return;

            if (args.Result == null) return;

            if (args.Result.GetType().FullName == controller.Model.GetType().FullName)
            {
                args.Result = controller.Model;
            }
        }
    }
}