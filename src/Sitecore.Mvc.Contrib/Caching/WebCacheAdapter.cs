using System;

namespace Sitecore.Mvc.Contrib.Caching
{
    public sealed class WebCacheAdapter : ICache
    {
        private readonly System.Web.Caching.Cache _cache;

        public WebCacheAdapter()
        {
            if (System.Web.HttpContext.Current != null)
                _cache = System.Web.HttpContext.Current.Cache;
            else
                throw new ArgumentNullException("No HttpContext, unable to use the web cache");
        }

        object ICache.this[string fieldName]
        {
            get { return _cache[fieldName]; }
            set { _cache[fieldName] = value; }
        }
    }
}