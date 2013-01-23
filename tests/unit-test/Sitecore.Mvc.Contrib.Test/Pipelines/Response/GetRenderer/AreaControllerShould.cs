using System;
using Moq;
using NUnit.Framework;

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;
using Sitecore.Mvc.Contrib.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Contrib.Presentation.Renderer;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;

using Assert = NUnit.Framework.Assert;

namespace Sitecore.Mvc.Contrib.Test.Pipelines.Response.GetRenderer
{
    [TestFixture]
    public class AreaControllerShould
    {
        private AreaController _controller ;
        private GetRendererArgs _args;

        [SetUp]
        public void SetUp()
        {
            _args = new GetRendererArgs(new Rendering()); 
            _controller = new AreaController();
        }

        [Test]
        public void ProcessShouldDoNothingIfRenderingIsNull()
        {
            // Arrange
            var contentRenderer = new ContentRenderer();
            _args.Result = contentRenderer;

            // Act
            _controller.Process(_args);

            // Assert
            Assert.That(_args.Result, Is.EqualTo(contentRenderer), "Rendering should not be re-assigned");
        }

        [Test]
        public void ProcessShouldDoNothingIfRenderingTemplateIsNull()
        {
            // Arrange
            _args.RenderingTemplate = null;

            // Act
            _controller.Process(_args);

            // Assert
            Assert.That(_args.Result, Is.Null, "Rendering should not be assigned");
        }

        [Test]
        public void ProcessShouldDoNothingIfRenderingTemplateIsNotDescendantOfAreaControllerTemplate()
        {
            // Arrange
            var builder = new Template.Builder("Dummy Template", new ID(Guid.NewGuid()), new TemplateCollection());

            _args.RenderingTemplate = builder.Template;

            // Act
            _controller.Process(_args);

            // Assert
            Assert.That(_args.Result, Is.Null, "Rendering should not be assigned");
        }


        [Test]
        public void ProcessShouldSetAreaControllerRendererIfDescendantOfAreaControllerTemplate()
        {
            // Arrange
            var builder = new Template.Builder("Area Controller Template", Constants.Templates.AreaController, new TemplateCollection());
            var fieldList = new FieldList
                {
                    {Constants.Fields.Controller.Action, "Index"},
                    {Constants.Fields.Controller.Name, "HelloWorld"},
                    {Constants.Fields.Controller.Area, "MyArea"}
                };
            var innerItem = new TestItem(fieldList);

            var rendering = new Rendering { RenderingItem = new RenderingItem(innerItem)};
            _args.Rendering = rendering;
            _args.RenderingTemplate = builder.Template;

            _controller.ControllerRunner = new Mock<IControllerRunner>().Object;

            // Act
            _controller.Process(_args);

            // Assert
            Assert.That(_args.Result, Is.InstanceOf<AreaControllerRenderer>(), "Rendering should be an AreaControllerRenderer");
        }
    }
}
