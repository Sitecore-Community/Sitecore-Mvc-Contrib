using System.IO;
using System.Web;
using Sitecore.Mvc.Contrib.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Pipelines;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc
{
    public static class SitecoreRendingHelper
    {
        public static HtmlString XsltRendering(this SitecoreHelper helpder, string path, bool varyByData = false, bool varyByDevice = false, bool varyByLogin = false, bool varyByUser = false, bool varyByParameters = false, bool varyByQueryString = false)
        {
            return RenderRendering("Xslt", null, varyByData, varyByDevice, varyByLogin, varyByParameters, varyByQueryString, varyByUser, "Path", path);
        }

        private static HtmlString RenderRendering(string renderingType, object parameters, bool varyByData, bool varyByDevice, bool varyByLogin, bool varyByUser, bool varyByParameters, bool varyByQueryString, params string[] defaultValues)
        {
            return RenderRendering(GetRendering(renderingType, parameters, defaultValues), varyByData, varyByDevice, varyByLogin, varyByParameters, varyByQueryString, varyByUser);
        }

        private static HtmlString RenderRendering(Rendering rendering, bool varyByData, bool varyByDevice, bool varyByLogin, bool varyByUser, bool varyByParameters, bool varyByQueryString)
        {
            var stringWriter = new StringWriter();
            var args = new CachedRenderingArgs(rendering, stringWriter)
            {
                VaryByData = varyByData,
                VaryByDevice = varyByDevice,
                VaryByLogin = varyByLogin,
                VaryByParameters = varyByParameters,
                VaryByQueryString = varyByQueryString,
                VaryByUser = varyByUser
            };
            PipelineService.Get().RunPipeline("mvc.renderRendering", args);
            return new HtmlString(stringWriter.ToString());
        }

        private static Rendering GetRendering(string renderingType, object parameters, params string[] defaultValues)
        {
            var rendering = new Rendering { RenderingType = renderingType };
            int index = 0;
            while (index < defaultValues.Length - 1)
            {
                rendering[defaultValues[index]] = defaultValues[index + 1];
                index += 2;
            }
            if (parameters != null)
                TypeHelper.GetProperties(parameters)
                    .Each(pair => rendering.Properties[pair.Key] = pair.Value.ValueOrDefault(o => o.ToString()));
            return rendering;
        }
    }

}
