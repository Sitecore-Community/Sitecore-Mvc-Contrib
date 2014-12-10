using System;
using Moq;
using NUnit.Framework;

using Sitecore.Data;
using Sitecore.Mvc.Contrib.Presentation;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Test.Presentation
{
    [TestFixture]
    class RenderingItemProviderShould
    {
        private RenderingItemProvider _sut;
        private Mock<IRenderingProvider> _renderingProvider;
        private Mock<IDatabase> _database;
        private TestItem _contextItem;
        private Mock<IRendering> _rendering;

        [SetUp]
        public void SetUp()
        {
            _renderingProvider = new Mock<IRenderingProvider>();
            _rendering = new Mock<IRendering>();
            _database = new Mock<IDatabase>();
            _contextItem = new TestItem();

            _renderingProvider.SetupGet(x => x.Rendering).Returns(_rendering.Object);

            _sut = new RenderingItemProvider(_renderingProvider.Object, _database.Object, _contextItem);
        }

        [Test]
        public void ReturnContextItemInPageItemProperty()
        {
            // Arrange

            // Act
            var item = _sut.PageItem;

            // Assert
            Assert.That(item, Is.EqualTo(_contextItem));
        }

        [Test]
        public void ReturnContextItemInItemPropertyWhenDataSourceNotSet()
        {
            // Arrange

            // Act
            var item = _sut.Item;

            // Assert
            Assert.That(item, Is.EqualTo(_contextItem));
        }

        [Test]
        public void ReturnContextItemInItemPropertyWhenDataSourceItemNotFound()
        {
            // Arrange
            _rendering.SetupGet(x => x.DataSource).Returns("/sitecore/content/home");

            // Act
            var item = _sut.Item;

            // Assert
            Assert.That(item, Is.EqualTo(_contextItem));
        }


        [Test]
        public void ReturnDataSourceItemInItemPropertyWhenDataSourceItemFound()
        {
            // Arrange
            var testItem = new TestItem();
            const string homeItemPath = "/sitecore/content/home";

            _rendering.SetupGet(x => x.DataSource).Returns(homeItemPath);
            _database.Setup(x => x.GetItem(homeItemPath)).Returns(testItem);

            // Act
            var item = _sut.Item;

            // Assert
            Assert.That(item, Is.EqualTo(testItem), "DataSource Item not returned");
        }

        [Test]
        public void CacheTheResultOfTheItemProperty()
        {
            // Arrange
            var testItem = new TestItem();
            const string homeItemPath = "/sitecore/content/home";

            _rendering.SetupGet(x => x.DataSource).Returns(homeItemPath);
            _database.Setup(x => x.GetItem(homeItemPath)).Returns(testItem);

            var item = _sut.Item;

            // Act
            item = _sut.Item;

            // Assert
            _database.Verify(x =>x.GetItem(homeItemPath), Times.Once());
        }
    }
}