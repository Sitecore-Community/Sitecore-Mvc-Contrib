using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Presentation
{
    public class PresentationRenderingProvider : IRenderingProvider
    {
        private readonly IRenderingContext _renderingContext;

        public PresentationRenderingProvider(IRenderingContext renderingContext)
        {
            _renderingContext = renderingContext;
        }

        public PresentationRenderingProvider() // Poor mans DI
            : this(new RenderingContextWrapper(RenderingContext.Current))
        {
        }

        public IRendering Rendering
        {
            get
            {
                if ((_renderingContext != null) && (_renderingContext.Rendering != null))
                {
                    return _renderingContext.Rendering;
                }

                return null;
            }
        }
    }
}