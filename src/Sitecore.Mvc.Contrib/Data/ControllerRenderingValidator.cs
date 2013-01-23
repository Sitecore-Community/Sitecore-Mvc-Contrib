using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

using Sitecore.Data;
using Sitecore.Data.Validators;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Mvc.Contrib.Extensions;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Mvc.Contrib.Data
{
    [Serializable]
    public class ControllerRenderingValidator : StandardValidator
    {
        private static readonly ID ControllerRenderingTemplate = new ID("{AB86861A-6030-46C5-B394-E8F99E8B87DB}");
        private static readonly IDictionary<string, Type> TypeLookupsByControllerFullName = new Dictionary<string, Type>();
        private static readonly Type ControllerType = typeof(SitecoreController);

        private static IEnumerable<Type> _controllers;
        private static readonly object SyncObject = new object();

        public ControllerRenderingValidator() { }
        protected ControllerRenderingValidator(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        protected override ValidatorResult Evaluate()
        {
            var item = GetItem();

            if (item.IsItemDerivedFromTemplate(ControllerRenderingTemplate))
            {
                return ValidatorResult.Valid;
            }

            var controllerName = item["controller"];
            var actionName = item["Controller Action"];

            var isValid = HasActionOnController(controllerName, actionName);

            Log.Info(string.Format("Validating controller rendering (controller {0}, action {1}) - {2}", controllerName, actionName, isValid), this);

            return isValid ? ValidatorResult.Valid : GetMaxValidatorResult();
        }

        private bool HasActionOnController(string controllerName, string actionName)
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

            return (Reflection.ReflectionUtil.GetMethod(controllerInstance, actionName, false, true, new object[] { }) != null);
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
            var controllers = FindDerivedTypes(assembly, ControllerType).ToArray();
            return controllers.FirstOrDefault(x => x.FullName == qualifiedClassName);
        }

        private Type GetControllerByNamingConvention(string controllerName)
        {
            var controllerFullName = string.Concat(controllerName, "Controller");
            return Controllers.FirstOrDefault(x => string.Compare(controllerFullName, x.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            Text = GetText(Translate.Text("Your controller is not defined properly"));
            return GetFailedResult(ValidatorResult.Warning);
        }

        public override string Name
        {
            get { return "ControllerRenderingValidator"; }
        }

        private static IEnumerable<Type> Controllers
        {
            get
            {
                if (_controllers == null)
                {
                    _controllers = GetControllerTypes();
                }

                return _controllers;
            }
        }

        private static IEnumerable<Type> GetControllerTypes()
        {
            var types = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var controllers = FindDerivedTypes(assembly, ControllerType).ToArray();

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

        private static IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
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