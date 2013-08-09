using Sitecore.Data.Items;

namespace Sitecore.Mvc.Contrib.Presentation
{
    public interface IRenderingItemProvider : IRenderingProvider
    {
        Item Item { get; }
        Item PageItem { get; }
    }
}