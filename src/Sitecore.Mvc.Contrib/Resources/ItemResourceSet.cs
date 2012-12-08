using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Mvc.Contrib.Html;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.Mvc.Contrib.Resources
{
    public class ItemResourceSet: ResourceSet
    {
        private readonly Item _item;

        public ItemResourceSet(Item item)
        {
            _item = item;
        }

        public override string GetString(string name)
        {
           return GetString(name, false);
        }

        public override string GetString(string name, bool ignoreCase)
        {
            return GetString(_item, name, ignoreCase);
        }

        public static string GetString(Item item, string fieldName, bool ignoreCase)
        {
            var field = item.Fields[fieldName];

            if (field == null) return string.Empty;

            FieldRenderer render = new FieldRenderer();
            render.Item = item;
            render.FieldName = field.Name;
            render.DisableWebEditing = !EditMode.UseEditMode;
            return render.Render();
        }



        public override object GetObject(string name, bool ignoreCase)
        {
            throw new NotSupportedException();
        }
    }
}
