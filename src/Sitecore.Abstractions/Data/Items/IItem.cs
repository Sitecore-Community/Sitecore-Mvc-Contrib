namespace Sitecore.Data.Items
{
    public interface IItem : IBaseItem
    {
        ITemplateItem Template { get; }
    }
}
