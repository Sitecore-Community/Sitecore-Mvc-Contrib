using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Mvc.Contrib.Extensions
{
    public static class TemplateExtensions
    {
        public static bool IsAncestorOrSelf(this ITemplateItem template, ID templateId)
        {
            if (template.ID == templateId)
            {
                return true;
            }

            if ((template.BaseTemplates == null) || (!template.BaseTemplates.Any()))
            {
                return false;
            }
            
            foreach (var baseTemplate in template.BaseTemplates)
            {
                if (baseTemplate.IsAncestorOrSelf(templateId))
                {
                    return true;
                }
            }

            return false;
        }       
    }
}