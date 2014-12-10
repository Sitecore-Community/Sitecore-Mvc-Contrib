using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Sitecore.Mvc.Contrib.Test
{
    /// <summary>
    /// Original code from http://blog.istern.dk/2012/02/07/unit-testing-with-sitecore-item/
    /// </summary>
    public class TestItem : Item
    {
        public TestItem(FieldList fieldList, string itemName = "dummy")
            : base(new ID(new Guid()), 
                   new ItemData(new ItemDefinition(new ID(new Guid()), itemName, new ID(new Guid()), new ID(new Guid())), Language.Invariant, new Sitecore.Data.Version(1), fieldList),
                     TestDatabaseFactory.Create())

        {
        }

        public TestItem() : this(new FieldList())
        {
            
        }
    }
}