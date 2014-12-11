using Moq;
using NUnit.Framework;

using Sitecore.Mvc.Contrib.Controllers;
using Sitecore.Mvc.Contrib.Presentation;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Test.Controllers
{
    [TestFixture]
    class ControllerBaseShould
    {
        private ControllerBase _sut;
        private Mock<IRenderingItemProvider> _renderingItemProvider;

        [SetUp]
        public void SetUp()
        {
            _renderingItemProvider = new Mock<IRenderingItemProvider>();

            _sut = new TestController(_renderingItemProvider.Object);
        }

        [Test]
        public void DeriveFromMvcControllerClass()
        {
            // Arrange

            // Act

            // Assert
            Assert.That(_sut, Is.InstanceOf<System.Web.Mvc.Controller>());
        }

        [Test]
        public void ReturnItemFromTheItemProperty()
        {
            // Arrange
            var testItem = new TestItem();
            _renderingItemProvider.SetupGet(x => x.Item).Returns(testItem);

            // Act
            var item = _sut.Item;

            // Assert
            Assert.That(item, Is.EqualTo(testItem));
        }

        [Test]
        public void ReturnAnItemFromThePageItemProperty()
        {
            // Arrange
            var testItem = new TestItem();
            _renderingItemProvider.SetupGet(x => x.PageItem).Returns(testItem);

            // Act
            var item = _sut.PageItem;

            // Assert
            Assert.That(item, Is.EqualTo(testItem));
        }

        [Test]
        public void Return_NameValueCollection_Of_Parameters()
        {
            // Arrange
            var rendering = new Mock<IRendering>();
            rendering.SetupGet(x => x["Parameters"]).Returns("Param1=Foo&Param2=Bar");

            _renderingItemProvider.SetupGet(x => x.Rendering).Returns(rendering.Object);

            // Act
            var parameters = _sut.Parameters;

            // Assert
            Assert.That(parameters.Count, Is.EqualTo(2));
            Assert.That(parameters["Param1"], Is.EqualTo("Foo"));
            Assert.That(parameters["Param2"], Is.EqualTo("Bar"));
        }
    }

    internal class TestController : ControllerBase
    {
        public TestController(IRenderingItemProvider renderingItemProvider)
            : base(renderingItemProvider)
        {
        }
    }
}
