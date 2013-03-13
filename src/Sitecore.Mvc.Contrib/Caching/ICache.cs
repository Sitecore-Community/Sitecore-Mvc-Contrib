namespace Sitecore.Mvc.Contrib.Caching
{
    public interface ICache
    {
        object this[string fieldName] { get; set; }
    }
}