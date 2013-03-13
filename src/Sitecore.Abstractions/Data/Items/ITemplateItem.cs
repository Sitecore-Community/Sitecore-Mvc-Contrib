using System.Collections.Generic;

namespace Sitecore.Data.Items
{
    public interface ITemplateItem : ICustomItemBase
    {
        IEnumerable<ITemplateItem> BaseTemplates { get; }
    }
}