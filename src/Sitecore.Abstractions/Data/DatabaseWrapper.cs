using Sitecore.Data.Items;

namespace Sitecore.Data
{
    public class DatabaseWrapper : IDatabase
    {
        private readonly Database _database;

        public DatabaseWrapper(Database database)
        {
            _database = database;
        }

        public Item GetItem(string path)
        {
            return _database.GetItem(path);
        }
    }
}