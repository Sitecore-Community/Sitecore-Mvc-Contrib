using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.Mvc.Contrib.Reflection
{
    public class AssemblyTypeFinder : ITypeFinder
    {
        public IEnumerable<Type> GetTypesDerivedFrom(Type baseType)
        {
            var types = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var controllers = FindDerivedTypes(assembly, baseType).ToArray();

                if (!controllers.Any()) continue;

                foreach (var controller in controllers.Where(controller => !types.Contains(controller)))
                {
                    types.Add(controller);
                }
            }

            return types.AsEnumerable();

            // Dev. Note: 
            // Equivalent LINQ is less maintainable
            //
            //            return AppDomain.CurrentDomain.GetAssemblies()
            //                .Select(assembly => FindDerivedTypes(assembly, controllerType).ToArray())
            //                .Where(controllers => controllers.Any())
            //                .SelectMany(controllers => controllers.Where(controller => !types.Contains(controller)));
        }

        public IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            try
            {
                return assembly.GetTypes().Where(t => t != baseType && baseType.IsAssignableFrom(t));
            }
            catch (ReflectionTypeLoadException)
            {
                return new Type[] { };
            }
        }
    }
}