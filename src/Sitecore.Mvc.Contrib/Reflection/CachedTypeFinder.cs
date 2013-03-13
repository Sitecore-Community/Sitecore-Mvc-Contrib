using System;
using System.Collections.Generic;
using System.Reflection;
using Sitecore.Mvc.Contrib.Caching;

namespace Sitecore.Mvc.Contrib.Reflection
{
    public class CachedTypeFinder : ITypeFinder
    {
        private readonly ITypeFinder _typeFinder;
        private static readonly object SyncLock = new object();
        private readonly CacheValues<IEnumerable<Type>> _cacheValues;

        public CachedTypeFinder(ICache cache, string fieldName, ITypeFinder typeFinder)
        {
            _cacheValues = new CacheValues<IEnumerable<Type>>(cache, fieldName);
            _typeFinder = typeFinder;
        }

        public IEnumerable<Type> GetTypesDerivedFrom(Type baseType)
        {
            const string field = "TypesDerivedFrom";

            if (_cacheValues[field] == null)
            {
                lock (SyncLock)
                {
                    _cacheValues[field] = _typeFinder.GetTypesDerivedFrom(baseType);
                }
            }
            return _cacheValues[field];
        }

        public IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            const string field = "DerivedTypes";

            if (_cacheValues[field] == null)
            {
                lock (SyncLock)
                {
                    _cacheValues[field] = _typeFinder.FindDerivedTypes(assembly, baseType);
                }
            }
            return _cacheValues[field];
        }
    }
}