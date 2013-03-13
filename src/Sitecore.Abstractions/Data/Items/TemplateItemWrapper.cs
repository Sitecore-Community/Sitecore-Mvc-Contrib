using System.Collections.Generic;

namespace Sitecore.Data.Items
{
    public class TemplateItemWrapper : ITemplateItem
    {
        private readonly TemplateItem _templateItem;

        public TemplateItemWrapper(TemplateItem templateItem)
        {
            _templateItem = templateItem;
        }

        public ID ID { get { return _templateItem.ID; } }

        public IEnumerable<ITemplateItem> BaseTemplates 
        { 
            get
            {
                foreach (var baseTemplate in _templateItem.BaseTemplates)
                {
                    yield return new TemplateItemWrapper(baseTemplate);
                }
            }
        }
    }
}