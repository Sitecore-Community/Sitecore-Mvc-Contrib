using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.Mvc.Contrib.Html
{
    public class EditMode : IDisposable
    {
        [ThreadStatic]
        private static bool _useEditMode;

        public static bool UseEditMode
        {
            get { return _useEditMode; }
        }

        public EditMode()
        {
            _useEditMode = true;
        }

        public void Dispose()
        {
            _useEditMode = false;
        }
    }
}
