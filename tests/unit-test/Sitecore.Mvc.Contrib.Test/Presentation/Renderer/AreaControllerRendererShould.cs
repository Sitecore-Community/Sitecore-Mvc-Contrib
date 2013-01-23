using System.IO;
using System.Text;

using Moq;
using NUnit.Framework;

using Sitecore.Mvc.Contrib.Presentation.Renderer;
using Sitecore.Mvc.Controllers;

namespace Sitecore.Mvc.Contrib.Test.Presentation.Renderer
{
    [TestFixture]
    public class AreaControllerRendererShould
    {
        private Mock<IControllerRunner> _controllerRunner;

        [SetUp]
        public void SetUp()
        {
            _controllerRunner = new Mock<IControllerRunner>();
        }

        [Test]
        public void RenderContent()
        {
            // Arrange
            var renderer = new AreaControllerRenderer(_controllerRunner.Object,
                                                      new AreaRouteData("controller", "action", "area"));
            var sb = new StringBuilder();
            var textwriter = new StringWriter(sb);

            _controllerRunner.Setup(x => x.Execute()).Returns("Some content");

            // Act
            renderer.Render(textwriter);

            // Assert
            Assert.That(sb.Length, Is.GreaterThan(0), "Content not rendered");
        }
    }
}
