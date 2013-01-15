using System;
using System.Web.Mvc;
using System.Web.Routing;

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Devices;

namespace Sitecore.Mvc.Presentation
{
    public class PageContextWrapper : IPageContext
    {
        private readonly PageContext _pageContext;

        public PageContextWrapper(PageContext pageContext)
        {
            if (pageContext == null) 
                throw new ArgumentNullException("pageContext");

            _pageContext = pageContext;
        }

        public Database Database { get { return _pageContext.Database; } }
        public Device Device { get { return _pageContext.Device; } }
        public HtmlHelper HtmlHelper { get { return _pageContext.HtmlHelper; } }
        public Item Item { get { return _pageContext.Item; } }
        public PageDefinition PageDefinition { get { return _pageContext.PageDefinition; } }
        public IView PageView { get { return _pageContext.PageView; } }
        public RequestContext RequestContext { get { return _pageContext.RequestContext; } }
    }
}
