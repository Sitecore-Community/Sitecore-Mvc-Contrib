using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Presentation
{
    public class RenderingItemProvider : IRenderingItemProvider
    {
        private readonly IRenderingProvider _renderingProvider;
        private readonly IDatabase _contextDatabase;
        private readonly Item _contextItem;
        private Item _item;

        public RenderingItemProvider(IRenderingProvider renderingProvider, IDatabase contextDatabase, Item contextItem)
        {
            _renderingProvider = renderingProvider;
            _contextDatabase = contextDatabase;
            _contextItem = contextItem;
        }

        public Item Item
        {
            get
            {
                if (_item == null)
                {
                    _item = GetItem();
                }
                return _item;
            }
        }

        private Item GetItem()
        {
            if (!string.IsNullOrEmpty(Rendering.DataSource))
            {
                return _contextDatabase.GetItem(Rendering.DataSource) ?? _contextItem;
            }

            return _contextItem;
        }

        public Item PageItem 
        {
            get
            {
                return _contextItem;
            }
        }

        public IRendering Rendering
        {
            get { return _renderingProvider.Rendering; }
        }
    }
}