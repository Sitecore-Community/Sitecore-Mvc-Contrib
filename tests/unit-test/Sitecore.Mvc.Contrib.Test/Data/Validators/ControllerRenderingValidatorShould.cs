using System;

using Moq;
using NUnit.Framework;

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Mvc.Contrib.Data.Validators;

using Assert = NUnit.Framework.Assert;

namespace Sitecore.Mvc.Contrib.Test.Data.Validators
{
    [TestFixture]
    public class ControllerRenderingValidatorShould
    {
        private TestControllerRenderingValidator _validator;
        private Mock<IItem> _currentItem;
        private Mock<ITemplateItem> _currentItemTemplate;
        private Mock<ITranslate> _translate;

        private Mock<IControllerValidator> _controlerValidator;
        private Mock<ILog> _log;

        private string _warningMessage = "";

        [SetUp]
        public void SetUp()
        {
            _currentItem = new Mock<IItem>();
            _currentItemTemplate = new Mock<ITemplateItem>();
            _translate = new Mock<ITranslate>();

            _controlerValidator = new Mock<IControllerValidator>();
            _log = new Mock<ILog>();

            _log.Setup(l => l.Warn(It.IsAny<string>(), It.IsAny<object>()))
                .Callback<string, object>((m, o) => _warningMessage = m);

            _translate.Setup(x => x.Text(It.IsAny<string>())).Returns((string s) => s.ToLower());

            _currentItem.SetupGet(x => x.Template).Returns(_currentItemTemplate.Object);

            _currentItemTemplate.SetupGet(x => x.ID).Returns(Constants.Templates.ControllerRenderingWithValidation);

            _validator = new TestControllerRenderingValidator(_controlerValidator.Object, _log.Object)
                             {
                                 Item = _currentItem.Object,
                                 Translate = _translate.Object,
                                 GetText = s => s
                             };
        }

        [Test]
        public void EvaluateReturnsValidForNonControllerRenderingTemplates()
        {
            // Arrange
            _currentItemTemplate.SetupGet(x => x.ID).Returns(new ID(Guid.NewGuid()));

            // Act
            var result = _validator.Evaluate();

            // Assert
            Assert.That(result, Is.EqualTo(ValidatorResult.Valid));
        }

        [Test]
        public void EvaluateReturnsWarningForMissingControllerName()
        {
            // Arrange
            _currentItemTemplate.SetupGet(x => x.ID).Returns(Constants.Templates.ControllerRenderingWithValidation);
            _currentItem.SetupGet(x => x["controller"]).Returns(string.Empty);

            // Act
            var result = _validator.Evaluate();

            // Assert
            Assert.That(result, Is.EqualTo(ValidatorResult.Warning));
        }

        [Test]
        public void EvaluateLogsMissingControllerName()
        {
            // Arrange
            _currentItem.SetupGet(x => x["controller"]).Returns(string.Empty);

            // Act
            _validator.Evaluate();

            // Assert
            Assert.That(_warningMessage.Contains("controller '<Empty>'"), Is.True, "Controller field diagnostic not provided");
        }

        [Test]
        public void EvaluateReturnsWarningForMissingActionName()
        {
            // Arrange
            _currentItem.SetupGet(x => x["Controller Action"]).Returns(string.Empty);

            // Act
            _validator.Evaluate();

            // Assert
            Assert.That(_warningMessage.Contains("action '<Empty>'"), Is.True, "Controller Action field diagnostic not provided");
        }
    }

    internal class TestControllerRenderingValidator : ControllerRenderingValidator
    {
        public TestControllerRenderingValidator(IControllerValidator controllerValidator, ILog log)
            : base(controllerValidator, log)
        {

        }

        public new ValidatorResult Evaluate()
        {
            return base.Evaluate();
        }
    }
}
