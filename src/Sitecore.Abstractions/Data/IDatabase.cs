using Sitecore.Data.Items;

namespace Sitecore.Data
{
    public interface IDatabase
    {
        Item GetItem(string path);
    }
}