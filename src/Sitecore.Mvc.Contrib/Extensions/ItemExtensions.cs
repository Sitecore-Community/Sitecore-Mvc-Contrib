using System.Linq;

using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Mvc.Contrib.Extensions
{
    public static class ItemExtensions
    {
        public static bool IsItemDerivedFromTemplate(this Item item, ID templateId)
        {
            var template = item.Template;

            return IsAncestorOrSelf(template, templateId);
        }

        private static bool IsAncestorOrSelf(TemplateItem template, ID templateId)
        {
            if (template.ID == templateId)
            {
                return true;
            }
            
            return template.BaseTemplates != null && 
                   template.BaseTemplates.Any(baseTemplate => IsAncestorOrSelf(baseTemplate, templateId));
        }
    }
}
