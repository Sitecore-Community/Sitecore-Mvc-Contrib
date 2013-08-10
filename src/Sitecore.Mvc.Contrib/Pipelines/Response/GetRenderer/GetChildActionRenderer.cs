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
            if (controllerAndAction == null)
            {
                return null;
            }
            string str = controllerAndAction.Item1;
            string str2 = controllerAndAction.Item2;
            return new ChildActionRenderer { ControllerName = str, ActionName = str2 };
        }
    }
}