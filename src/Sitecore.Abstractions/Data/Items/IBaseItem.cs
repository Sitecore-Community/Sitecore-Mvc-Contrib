namespace Sitecore.Data.Items
{
    public interface IBaseItem
    {
//        FieldCollection Fields { get; }

        string this[string fieldName] { get; set; }

        string this[int index] { get; set; }

        string this[ID fieldID] { get; set; }
    }
}