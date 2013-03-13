using System;
using System.Reflection;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Sitecore.Mvc.Contrib.Data.Validators;
using Sitecore.Mvc.Contrib.Reflection;

namespace Sitecore.Mvc.Contrib.Test.Data.Validators
{
    [TestFixture]
    public class ControllerValidatorShould
    {
        private ControllerValidator _validator;

        private Mock<ITypeFinder> _typeFinder; 

        [SetUp]
        public void SetUp()
        {
            _typeFinder = new Mock<ITypeFinder>();

            _validator = new ControllerValidator(_typeFinder.Object);
        }

        [Test]
        public void ReturnFalseWhenNoControllerNameIsWhitespace()
        {
            // Arrange
            const string controller = " ";
            const string action = "Index";

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnFalseWhenNoActionNameIsWhitespace()
        {
            // Arrange
            const string controller = "Test";
            const string action = " ";

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnFalseWhenNoControllersLocated()
        {
            // Arrange
            const string controller = "Test";
            const string action = "Index";

            _typeFinder.Setup(x => x.GetTypesDerivedFrom(It.IsAny<Type>()))
                       .Returns(new Type[] {  });

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnFalseForMissingControllerNameAndActionByConvention()
        {
            // Arrange
            const string controller = "Test";
            const string action = "Index";

            _typeFinder.Setup(x => x.GetTypesDerivedFrom(It.IsAny<Type>()))
                       .Returns(new[] { typeof(TestController) });

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnFalseForControllerThatCantBeInstantiated()
        {
            // Arrange
            const string controller = "Private";
            const string action = "Method";

            _typeFinder.Setup(x => x.GetTypesDerivedFrom(It.IsAny<Type>()))
                       .Returns(new[] { typeof(PrivateController) });

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnTrueForControllerNameAndActionByConvention()
        {
            // Arrange
            const string controller = "Test";
            const string action = "Method";

            _typeFinder.Setup(x => x.GetTypesDerivedFrom(It.IsAny<Type>()))
                       .Returns(new[] {typeof (TestController)});

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnTrueForFullyQualifiedControllerNameAndAction()
        {
            // Arrange
            const string controller = "Sitecore.Mvc.Contrib.Test.Data.Validators.TestController, Sitecore.Mvc.Contrib.Test";
            const string action = "Method";

            _typeFinder.Setup(x => x.FindDerivedTypes(It.IsAny<Assembly>(), It.IsAny<Type>()))
                       .Returns(new[] { typeof(TestController) });

            // Act
            var result = _validator.ActionExistsOnController(controller, action);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void SearchesForFullyQualifiedControllerNameAndActionAreCached()
        {
            // Arrange
            const string controller = "Sitecore.Mvc.Contrib.Test.Data.Validators.TestController, Sitecore.Mvc.Contrib.Test";
            const string action = "Method";

            _typeFinder.Setup(x => x.FindDerivedTypes(It.IsAny<Assembly>(), It.IsAny<Type>()))
                       .Returns(new[] { typeof(TestController) });

            // Act
            _validator.ActionExistsOnController(controller, action);
            _validator.ActionExistsOnController(controller, action);

            // Assert
            _typeFinder.Verify(t => t.FindDerivedTypes(It.IsAny<Assembly>(), It.IsAny<Type>()), Times.AtMostOnce());
        }
    }


    internal class TestController
    {
        public ActionResult Method()
        {
            return new EmptyResult();
        }
    }

    internal class PrivateController
    {
        private PrivateController()
        {

        }
    }
}