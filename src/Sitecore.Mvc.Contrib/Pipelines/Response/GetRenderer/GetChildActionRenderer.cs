using Sitecore.Data.Templates;
using Sitecore.Mvc.Contrib.Presentation.Renderer;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using System;

namespace Sitecore.Mvc.Contrib.Pipelines.Response.GetRenderer
{
    public class ChildAction : GetControllerRenderer
    {
        protected override Mvc.Presentation.Renderer GetRenderer(Mvc.Presentation.Rendering rendering, GetRendererArgs args)
        {
            Tuple<string, string> controllerAndAction = this.GetControllerAndAction(rendering, args);
            if (!IsChildActionRendering(args.RenderingTemplate) || controllerAndAction == null)
            {
                return null;
            }
            string controller = controllerAndAction.Item1;
            string action = controllerAndAction.Item2;
            return new ChildActionRenderer { ControllerName = controller, ActionName = action };
        }

        private static bool IsChildActionRendering(Template renderingTemplate)
        {
            return ((renderingTemplate != null) &&
                    (renderingTemplate.DescendsFromOrEquals(Constants.Templates.ChildActionRendering)));
        }
    }
}