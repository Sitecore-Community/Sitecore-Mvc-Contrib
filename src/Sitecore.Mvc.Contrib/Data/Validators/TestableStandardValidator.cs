using System;
using System.Runtime.Serialization;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Globalization;

namespace Sitecore.Mvc.Contrib.Data.Validators
{
    public abstract class TestableStandardValidator : StandardValidator
    {
        private IItem _item;
        private ITranslate _translate;

        protected TestableStandardValidator()
        {
            GetText = v => base.GetText(v);
        }

        protected TestableStandardValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            GetText = v => base.GetText(v);
        }

        public new Func<string, string> GetText;

        public IItem Item
        {
            get { return _item ?? (_item = new ItemWrapper(GetItem())); }
            set { _item = value; }
        }

        public ITranslate Translate
        {
            get { return _translate ?? (_translate = new TranslateWrapper()); }
            set { _translate = value; }
        }
    }
}