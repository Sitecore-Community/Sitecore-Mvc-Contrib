using Sitecore.Data.Items;
using Sitecore.Mvc.Contrib.Controllers;
using Sitecore.Mvc.Contrib.Presentation.Renderer;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Data.Templates;

namespace Sitecore.Mvc.Contrib.Pipelines.Response.GetRenderer
{
    public class AreaController : GetRendererProcessor
    {
        public IControllerRunner ControllerRunner { get; set; }  // Required to test

        public override void Process(GetRendererArgs args)
        {
            if (args.Result != null)
            {
                return;
            }

            if (! IsAreaControllerRendering(args.RenderingTemplate))
            {
                return;
            }

            args.Result = GetRenderer(args.Rendering, args);
        }

        private static bool IsAreaControllerRendering(Template renderingTemplate)
        {
            return ((renderingTemplate != null) &&
                    (renderingTemplate.DescendsFromOrEquals(Constants.Templates.AreaController)));
        }

        protected virtual Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
        {
            var areaRouteData = GetAreaRouteDataFromRenderingItem(rendering.RenderingItem);
            var controllerRunner = GetControllerRunner(areaRouteData);

            return new AreaControllerRenderer(controllerRunner, areaRouteData);
        }

        private IControllerRunner GetControllerRunner(AreaRouteData areaRouteData)
        {
            return ControllerRunner ?? new ControllerRunnerWrapper(new AreaControllerRunner(areaRouteData));
        }

        private static AreaRouteData GetAreaRouteDataFromRenderingItem(RenderingItem renderingItem)
        {
            var fields = renderingItem.InnerItem.Fields;
            var action = fields[Constants.Fields.Controller.Action].GetValue(true);
            var controller = fields[Constants.Fields.Controller.Name].GetValue(true);
            var area = fields[Constants.Fields.Controller.Area].GetValue(true);
            return new AreaRouteData(controller, action, area);
        }
    }
}
