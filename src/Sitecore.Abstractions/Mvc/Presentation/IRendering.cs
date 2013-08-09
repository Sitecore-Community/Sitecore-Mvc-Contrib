namespace Sitecore.Mvc.Presentation
{
    public interface IRendering
    {
        string DataSource { get; set; }

        string this[string propertyName] { set; get; }
    }
}