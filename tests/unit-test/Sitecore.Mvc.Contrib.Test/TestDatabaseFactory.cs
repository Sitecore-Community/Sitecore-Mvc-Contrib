using Sitecore.Data;

namespace Sitecore.Mvc.Contrib.Test
{
    public static class TestDatabaseFactory
    {
        public static Database Create()
        {
            return (Database)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Database));
        }
    }
}