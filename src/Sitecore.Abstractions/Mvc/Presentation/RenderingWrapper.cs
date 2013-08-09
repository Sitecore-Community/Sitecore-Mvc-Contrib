namespace Sitecore.Mvc.Presentation
{
    public class RenderingWrapper : IRendering
    {
        private readonly Rendering _rendering;

        public RenderingWrapper(Rendering rendering)
        {
            _rendering = rendering;
        }

        public string DataSource { get; set; }

        public string this[string propertyName]
        {
            get { return _rendering[propertyName]; }
            set { _rendering[propertyName] = value; }
        }
    }
}