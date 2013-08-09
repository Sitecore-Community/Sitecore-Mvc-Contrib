using System.Collections.Specialized;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Contrib.Presentation;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Controllers
{
    public abstract class ControllerBase
    {
        private readonly IRenderingItemProvider _renderingItemProvider;

        protected ControllerBase(IRenderingItemProvider renderingItemProvider)
        {
            _renderingItemProvider = renderingItemProvider;
        }

        protected ControllerBase() // Poor mans DI
            : this(new RenderingItemProvider(new PresentationRenderingProvider(), new DatabaseWrapper(Sitecore.Context.Database), Sitecore.Context.Item))
        {
        }

        public Item Item
        {
            get { return _renderingItemProvider.Item; }
        }

        public Item PageItem
        {
            get { return _renderingItemProvider.PageItem; }
        }

        public NameValueCollection Parameters
        {
            get
            {
                if (Rendering != null)
                {
                    var parameters = Rendering["Parameters"];

                    if (!string.IsNullOrEmpty(parameters))
                    {
                        return Web.WebUtil.ParseUrlParameters(parameters);
                    }
                }

                return new NameValueCollection();
            }
        }

        private IRendering Rendering
        {
            get { return _renderingItemProvider.Rendering; }
        }
    }
}