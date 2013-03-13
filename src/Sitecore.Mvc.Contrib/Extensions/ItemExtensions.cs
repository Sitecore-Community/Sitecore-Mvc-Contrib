using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Mvc.Contrib.Extensions
{
    public static class ItemExtensions
    {
        public static bool IsItemDerivedFromTemplate(this IItem item, ID templateId)
        {
            var template = item.Template;

            return template.IsAncestorOrSelf(templateId);
        }
    }
}
