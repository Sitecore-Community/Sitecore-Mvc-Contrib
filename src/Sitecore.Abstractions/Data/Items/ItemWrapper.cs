namespace Sitecore.Data.Items
{
    public sealed class ItemWrapper : IItem
    {
        private readonly Item _item;

        public ItemWrapper(Item item)
        {
            _item = item;
        }

        public ITemplateItem Template { get { return new TemplateItemWrapper(_item.Template); } }

        string IBaseItem.this[string fieldName]
        {
            get { return _item[fieldName]; }
            set { _item[fieldName] = value; }
        }

        string IBaseItem.this[int index]
        {
            get { return _item[index]; }
            set { _item[index] = value; }
        }

        string IBaseItem.this[ID fieldID]
        {
            get { return _item[fieldID]; }
            set { _item[fieldID] = value; }
        }
    }
}