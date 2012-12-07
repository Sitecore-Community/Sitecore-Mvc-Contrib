using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Sitecore.Mvc.Contrib.Resources
{
    public class DictionaryResourceSet: ItemResourceSet
    {
        private const string Key = "Key";
        private const string Phrase = "Phrase";

        private readonly Item _item;

        public DictionaryResourceSet(Item item) : base(null)
        {
            _item = item;
        }

        public override string GetString(string name)
        {
            return GetString(name, false);
        }
        public override string GetString(string name, bool ignoreCase)
        {
            foreach (Item entry in _item.Children)
            {
                if (entry[Key] == name)
                {
                    return GetString(entry, Phrase, ignoreCase);
                }
            }
        
            return string.Empty;
        }

        public override object GetObject(string name, bool ignoreCase)
        {
            throw new NotSupportedException();
        }
        
    }
}
