using System;
using System.Runtime.Serialization;

using Sitecore.Data.Validators;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Contrib.Caching;
using Sitecore.Mvc.Contrib.Extensions;
using Sitecore.Mvc.Contrib.Reflection;

namespace Sitecore.Mvc.Contrib.Data.Validators
{
    [Serializable]
    public class ControllerRenderingValidator : TestableStandardValidator
    {
        private readonly IControllerValidator _controllerValidator;
        private readonly ILog _log;

        public ControllerRenderingValidator(IControllerValidator controllerValidator, ILog log)
        {
            _controllerValidator = controllerValidator;
            _log = log;
        }

        public ControllerRenderingValidator() 
            : this(new ControllerValidator(new CachedTypeFinder(new WebCacheAdapter(), "Sitecore Controllers", new AssemblyTypeFinder())), 
                   new LogWrapper())  // TODO poor man's DI
        {
        }

        protected ControllerRenderingValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected override ValidatorResult Evaluate()
        {
            if (!Item.IsItemDerivedFromTemplate(Constants.Templates.ControllerRenderingWithValidation))
            {
                return ValidatorResult.Valid;
            }

            var controllerName = Item["controller"];
            var actionName = Item["Controller Action"];

            var isValid = _controllerValidator.ActionExistsOnController(controllerName, actionName);

            if (!isValid)
            {
                const string empty = "<Empty>";

                if (string.IsNullOrWhiteSpace(controllerName))
                {
                    controllerName = empty;
                }

                if (string.IsNullOrWhiteSpace(actionName))
                {
                    actionName = empty;
                }

                _log.Warn(string.Format("Validating controller rendering (controller '{0}', action '{1}')", controllerName, actionName), this);           
            }

            return isValid ? ValidatorResult.Valid : GetMaxValidatorResult();
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            var errorMessage = Translate.Text("Your controller is not defined properly");
            Text = GetText(errorMessage);
            return GetFailedResult(ValidatorResult.Warning);
        }

        public override string Name
        {
            get { return "ControllerRenderingValidator"; }
        }
    }
}