using System.IO;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Pipelines.Response.RenderRendering
{
    public class CachedRenderingArgs : RenderRenderingArgs
    {
        public bool VaryByData { get; set; }
        public bool VaryByDevice { get; set; }
        public bool VaryByLogin { get; set; }
        public bool VaryByUser { get; set; }
        public bool VaryByParameters { get; set; }
        public bool VaryByQueryString { get; set; }
        public CachedRenderingArgs(Rendering rendering, TextWriter writer)
            : base(rendering, writer)
        {
        }
    }
}
