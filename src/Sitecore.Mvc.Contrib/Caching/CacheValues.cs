namespace Sitecore.Mvc.Contrib.Caching
{
    public class CacheValues<T>
    {
        private readonly ICache _cache;
        private readonly string _fieldNameBase;

        public CacheValues(ICache cache, string fieldNameBase)
        {
            _cache = cache;
            _fieldNameBase = fieldNameBase;
        }

        public T this[string field]
        {
            get { return (T) _cache[_fieldNameBase + field]; }
            set { _cache[_fieldNameBase + field] = value; }
        }
    }
}