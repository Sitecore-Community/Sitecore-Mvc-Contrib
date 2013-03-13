using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sitecore.Mvc.Contrib.Reflection
{
    public interface ITypeFinder
    {
        IEnumerable<Type> GetTypesDerivedFrom(Type baseType);
        IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType);
    }
}