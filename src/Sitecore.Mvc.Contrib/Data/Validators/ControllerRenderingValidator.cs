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
                   new LogWrapper())  // Poor man's DI
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Dev. Note: [Serializable] will raise a CA2240 code analysis warning.
            //            To fix a violation of this rule, make the GetObjectData method visible and overridable and make sure all 
            //            instance fields are included in the serialization process or explicitly marked with the 
            //            NonSerializedAttributeattribute.

            base.GetObjectData(info, context);
        }
    }
}