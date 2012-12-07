using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using Sitecore.Data.Managers;

namespace Sitecore.Mvc.Contrib.Resources
{

    public class SitecoreResourceManager : System.Resources.ResourceManager
    {
        private readonly string _path;
        private readonly bool _dictionary;

        public SitecoreResourceManager(string path, bool dictionary = false)
        {
            _path = path;
            _dictionary = dictionary;
            ResourceSets = new Hashtable();
        }

        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            var lang = LanguageManager.GetLanguages(Sitecore.Context.Database)
                          .FirstOrDefault(x => x.CultureInfo.Equals(culture)) ?? LanguageManager.DefaultLanguage;

            var db = Sitecore.Context.Database;
            var item = db.GetItem(_path, lang);

            ResourceSet rs = null;

            if (ResourceSets.Contains(culture.Name))
            {
                rs = ResourceSets[culture.Name] as ResourceSet;
            }
            else if (_dictionary)
            {
                rs = new DictionaryResourceSet(item);
                ResourceSets.Add(culture.Name, rs);
            }
            else
            {
                rs = new ItemResourceSet(item);
            }

            return rs;
        }
    }
}
