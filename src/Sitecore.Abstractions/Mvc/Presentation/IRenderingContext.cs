using Sitecore.Data.Items;

namespace Sitecore.Mvc.Presentation
{
    public interface IRenderingContext
    {
        IItem ContextItem { get; }
        IRendering Rendering { get; }
    }
}