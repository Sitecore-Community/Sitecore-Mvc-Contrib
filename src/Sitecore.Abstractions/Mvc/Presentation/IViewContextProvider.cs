using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Sitecore.Mvc.Presentation
{
    public interface IViewContextProvider
    {
        ViewContext GetCurrentViewContext();
    }
}
