using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Sitecore.Diagnostics;
using Sitecore.Mvc.Contrib.Reflection;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Mvc.Contrib.Data.Validators
{
    public class ControllerValidator : IControllerValidator
    {
        public static readonly Type SitecoreControllerType = typeof(SitecoreController); 
        
        private readonly ITypeFinder _typeFinder;
        private static readonly IDictionary<string, Type> TypeLookupsByControllerFullName = new Dictionary<string, Type>();
        private static readonly object SyncObject = new object();

        public ControllerValidator(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public bool ActionExistsOnController(string controllerName, string actionName)
        {
            if ((string.IsNullOrWhiteSpace(controllerName)) || (string.IsNullOrWhiteSpace(actionName)))
            {
                return false;
            }

            try
            {
                var controller = GetController(controllerName);

                if (controller == null)
                {
                    return false;
                }

                var controllerInstance = controller.Assembly.CreateInstance(controller.FullName);

                if (controllerInstance == null)
                {
                    return false;
                }            
                
                return (Sitecore.Reflection.ReflectionUtil.GetMethod(controllerInstance, actionName, false, true, new object[] { }) != null);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Type GetController(string controllerName)
        {
            return TypeHelper.LooksLikeTypeName(controllerName)
                       ? GetTypeFromFullName(controllerName)
                       : GetControllerByNamingConvention(controllerName);
        }

        private Type GetTypeFromFullName(string assemblyQualifiedFullName)
        {
            if (!TypeLookupsByControllerFullName.ContainsKey(assemblyQualifiedFullName))
            {
                Log.Info(string.Format("Attempting to load {0}", assemblyQualifiedFullName), this);  // TODO remove
                lock (SyncObject)
                {
                    var type = GetTypeFromAssemblyByFullName(assemblyQualifiedFullName);

                    TypeLookupsByControllerFullName[assemblyQualifiedFullName] = type;

                    return type;
                }
            }

            return TypeLookupsByControllerFullName[assemblyQualifiedFullName];
        }

        private Type GetTypeFromAssemblyByFullName(string assemblyQualifiedFullName)
        {
            var fullnameComponents = assemblyQualifiedFullName.Split(new[] { ',' });
            var assemblyName = fullnameComponents.Last().Trim();
            var qualifiedClassName = fullnameComponents.First().Trim();

            var assembly = Assembly.Load(assemblyName);
            var controllers = _typeFinder.FindDerivedTypes(assembly, SitecoreControllerType).ToArray();
            return controllers.FirstOrDefault(x => x.FullName == qualifiedClassName);
        }

        private Type GetControllerByNamingConvention(string controllerName)
        {
            var controllerFullName = string.Concat(controllerName, "Controller");
            var controllers = _typeFinder.GetTypesDerivedFrom(SitecoreControllerType);

            return controllers.FirstOrDefault(x => string.Compare(controllerFullName, x.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}