using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Presentation
{
    public interface IRenderingProvider
    {
        IRendering Rendering { get; }
    }
}