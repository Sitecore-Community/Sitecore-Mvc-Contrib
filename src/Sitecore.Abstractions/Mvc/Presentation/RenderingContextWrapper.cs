using Sitecore.Data.Items;

namespace Sitecore.Mvc.Presentation
{
    public class RenderingContextWrapper : IRenderingContext
    {
        private readonly RenderingContext _renderingContext;

        public RenderingContextWrapper(RenderingContext renderingContext)
        {
            _renderingContext = renderingContext;
        }

        public IItem ContextItem
        {
            get { return new ItemWrapper(_renderingContext.ContextItem); }
        }

        public IRendering Rendering
        {
            get { return new RenderingWrapper(_renderingContext.Rendering); }
        }
    }
}