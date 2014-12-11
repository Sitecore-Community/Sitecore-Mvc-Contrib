using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using StructureMap;

namespace Sitecore.Mvc.Contrib.StructureMap
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }
            
            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>()

                .Where(s => s.GetType() == serviceType);
        }

        private readonly IContainer _container;
    }
}