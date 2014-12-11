using Moq;
using NUnit.Framework;

using Sitecore.Mvc.Contrib.Presentation.Renderer;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using System.IO;
using System.Web;
using System.Web.Routing;
using Sitecore.Mvc.Common;

namespace Sitecore.Mvc.Contrib.Test.Presentation.Renderer
{
    [TestFixture]
    public class ChildActionRendererShould
    {
        Mock<HtmlHelper> htmlHelper;
        Mock<HttpContextBase> httpContext;
        Mock<RouteBase> route;
        Mock<IViewDataContainer> viewDataContainer;
        Mock<IPageContext> pageContext;

        RouteData originalRouteData;
        RouteCollection routes;
        ViewContext viewContext;
        VirtualPathData virtualPathData;

        [TestFixtureSetUp]
        public void Setup()
        {
            route = new Mock<RouteBase>();
            route
                .Setup(r => r.GetVirtualPath(It.IsAny<RequestContext>(), It.IsAny<RouteValueDictionary>()))
                .Returns(() => virtualPathData);

            virtualPathData = new VirtualPathData(route.Object, "~/VirtualPath");

            routes = new RouteCollection();
            routes.Add(route.Object);

            originalRouteData = new RouteData();

            string returnValue = "";
            httpContext = new Mock<HttpContextBase>();
            httpContext
                .Setup(hc => hc.Request.ApplicationPath).Returns("~");
            httpContext
                .Setup(hc => hc.Response.ApplyAppPathModifier(It.IsAny<string>()))
                .Callback<string>(s => returnValue = s)
                .Returns(() => returnValue);
            httpContext
                .Setup(hc => hc.Server.Execute(It.IsAny<IHttpHandler>(), It.IsAny<TextWriter>(), It.IsAny<bool>()));

            viewContext = new ViewContext
            {
                RequestContext = new RequestContext(httpContext.Object, originalRouteData)
            };

            viewDataContainer = new Mock<IViewDataContainer>();

            htmlHelper = new Mock<HtmlHelper>(viewContext, viewDataContainer.Object, routes);

            var requestContext = new RequestContext(httpContext.Object, originalRouteData);
            ContextService.Get().Push(new PageContext { RequestContext = requestContext });

            pageContext = new Mock<IPageContext>();
            pageContext
                .Setup(x => x.RequestContext)
                .Returns(requestContext);

        }

        [Test]
        public void RenderContentWhenActionAndControllerAreValidStrings()
        {
            // Arrange
            var renderer = new ChildActionRenderer(pageContext.Object, htmlHelper.Object);
            var textwriterMock = new Mock<TextWriter>();
            renderer.ActionName = "Index";
            renderer.ControllerName = "Test";

            // Act
            renderer.Render(textwriterMock.Object);

            // Assert
            textwriterMock.Verify(tw => tw.Write(It.IsAny<HtmlString>()));
        }

        [Test]
        public void NotRenderContentIfControllerIsNull()
        {
            // Arrange
            var renderer = new ChildActionRenderer(pageContext.Object, htmlHelper.Object);
            var textwriterMock = new Mock<TextWriter>();
            renderer.ActionName = "Index";
            renderer.ControllerName = null;

            // Act
            renderer.Render(textwriterMock.Object);

            // Assert
            textwriterMock.Verify(tw => tw.Write(It.IsAny<string>()), Times.Never());
        }
        [Test]

        public void NotRenderContentIfControllerEmptyString()
        {
            // Arrange
            var renderer = new ChildActionRenderer(pageContext.Object, htmlHelper.Object);
            var textwriterMock = new Mock<TextWriter>();
            renderer.ActionName = "Index";
            renderer.ControllerName = "";

            // Act
            renderer.Render(textwriterMock.Object);

            // Assert
            textwriterMock.Verify(tw => tw.Write(It.IsAny<string>()), Times.Never());
        }
        public void NotRenderContentIfActionIsNull()
        {
            // Arrange
            var renderer = new ChildActionRenderer(pageContext.Object, htmlHelper.Object);
            var textwriterMock = new Mock<TextWriter>();
            renderer.ActionName = null;
            renderer.ControllerName = "Test";

            // Act
            renderer.Render(textwriterMock.Object);

            // Assert
            textwriterMock.Verify(tw => tw.Write(It.IsAny<string>()), Times.Never());
        }
        [Test]

        public void NotRenderContentIfActionEmptyString()
        {
            // Arrange
            var renderer = new ChildActionRenderer(pageContext.Object, htmlHelper.Object);
            var textwriterMock = new Mock<TextWriter>();
            renderer.ActionName = "";
            renderer.ControllerName = "Test";

            // Act
            renderer.Render(textwriterMock.Object);

            // Assert
            textwriterMock.Verify(tw => tw.Write(It.IsAny<string>()), Times.Never());
        }
    }
}
