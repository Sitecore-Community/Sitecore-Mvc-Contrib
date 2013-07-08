using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;
using NUnit.Framework;

using Sitecore.Diagnostics;
using Sitecore.Mvc.Contrib.Pipelines.MvcEvents;
using Sitecore.Mvc.Devices;
using Sitecore.Mvc.Pipelines.MvcEvents.ResultExecuting;
using Sitecore.Mvc.Presentation;

using Assert = NUnit.Framework.Assert;

namespace Sitecore.Mvc.Contrib.Test.Pipelines.MvcEvents
{
    [TestFixture]
    public class InjectionFilterShould
    {
        private InjectViewInPlaceholderFilter _filter;
        private Mock<IPageContext> _pageContext;
        private Mock<ILog> _logger;
        private Mock<HttpContextBase> _httpContext;
        private Mock<Controller> _controller;
        private Mock<IViewEngine> _viewEngine;
        private Mock<IView> _view;
        private ViewResult _viewResult;
        private ViewEngineResult _viewEngineResult;
        private Dictionary<string, object> _itemsDictionary;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILog>();

            _pageContext = new Mock<IPageContext>();
            _pageContext.SetupAllProperties();
            _pageContext.SetupGet(x => x.PageDefinition).Returns(new PageDefinition());

            _filter = new InjectViewInPlaceholderFilter(_logger.Object) { PageContext = _pageContext.Object };

            _httpContext = new Mock<HttpContextBase>();
            _httpContext.SetupAllProperties();
            _itemsDictionary = new Dictionary<string, object>();
            _httpContext.SetupGet(x => x.Items).Returns(_itemsDictionary);

            _controller = new Mock<Controller>();
            _controller.SetupAllProperties();
            _controller.Object.ControllerContext = new ControllerContext(_httpContext.Object, new RouteData(), _controller.Object);

            _viewEngine = new Mock<IViewEngine>();
            _view = new Mock<IView>();

            _viewResult = new ViewResult
                {
                    ViewEngineCollection = new ViewEngineCollection { _viewEngine.Object }
                };

            _viewEngineResult = new ViewEngineResult(_view.Object, _viewEngine.Object);
        }
        
        [Test]
        public void ProcessShouldIgnoreNullViewResults()
        {
            // Arrange
            var args = new ResultExecutingArgs(new ResultExecutingContext());

            // Act
            _filter.Process(args);

            // Assert
            Assert.That(_pageContext.Object.PageDefinition.Renderings.Count, Is.EqualTo(0), "Rendering should not have been added to the page definition");
        }

        [Test]
        public void ProcessShouldIgnoreNonViewResults()
        {
            // Arrange
            var resultExecutingContext = new ResultExecutingContext(_controller.Object.ControllerContext, new ContentResult());
            var args = new ResultExecutingArgs(resultExecutingContext);

            // Act
            _filter.Process(args);

            // Assert
            Assert.That(_pageContext.Object.PageDefinition.Renderings.Count, Is.EqualTo(0), "Rendering should not have been added to the page definition");
        }

        [Test]
        public void ProcessShouldIgnoreRoutesWithoutPlaceholder()
        {
            // Arrange
            var resultExecutingContext = new ResultExecutingContext(_controller.Object.ControllerContext, new ViewResult());
            var args = new ResultExecutingArgs(resultExecutingContext);

            // Act
            _filter.Process(args);

            // Assert
            Assert.That(_pageContext.Object.PageDefinition.Renderings.Count, Is.EqualTo(0), "Rendering should not have been added to the page definition");
        }

        [Test]
        public void UsePartialViewsByDefault()
        {
            // Arrange
            var args = ResultExecutingArgsBuilder(_viewResult);
            args.Context.RouteData.Values["scPlaceholder"] = "main";
            args.Context.RouteData.Values["action"] = "MyView";

            _pageContext
                .SetupGet(x => x.Item)
                .Returns(new TestItem());

            _pageContext
                .SetupGet(x => x.Device)
                .Returns(new Device(Guid.NewGuid()));

            _viewEngine
                .Setup(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_viewEngineResult);

            // Act
            _filter.Process(args);

            // Assert
            _viewEngine.Verify(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), true), Times.Exactly(1));
        }

        [Test]
        public void UseViewIfPartialViewIsSetFalseInRoute()
        {
            // Arrange
            var args = ResultExecutingArgsBuilder(_viewResult);
            args.Context.RouteData.Values["scPlaceholder"] = "main";
            args.Context.RouteData.Values["action"] = "MyView";
            args.Context.RouteData.Values["partialView"] = "false";

            _pageContext
                .SetupGet(x => x.Item)
                .Returns(new TestItem());

            _pageContext
                .SetupGet(x => x.Device)
                .Returns(new Device(Guid.NewGuid()));

            _viewEngine
                .Setup(x => x.FindView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_viewEngineResult);

            // Act
            _filter.Process(args);

            // Assert
            _viewEngine.Verify(x => x.FindView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<string>(), true), Times.Exactly(1));
        }

        [Test]
        public void UsePartialViewsIfPartialViewIsInvalidValueInRoute()
        {
            // Arrange
            var args = ResultExecutingArgsBuilder(_viewResult);
            args.Context.RouteData.Values["scPlaceholder"] = "main";
            args.Context.RouteData.Values["action"] = "MyView";
            args.Context.RouteData.Values["partialView"] = "notBool";

            _pageContext
                .SetupGet(x => x.Item)
                .Returns(new TestItem());

            _pageContext
                .SetupGet(x => x.Device)
                .Returns(new Device(Guid.NewGuid()));

            _viewEngine
                .Setup(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_viewEngineResult);

            // Act
            _filter.Process(args);

            // Assert
            _viewEngine.Verify(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), true), Times.Exactly(1));
        }

        [Test]
        public void AddContentRenderingToCurrentPageDefinitionByViewName()
        {
            // Arrange
            _viewResult.ViewName = "FooBarView";

            var args = ResultExecutingArgsBuilder(_viewResult);
            args.Context.RouteData.Values["scPlaceholder"] = "main";
            
            _pageContext
                .SetupGet(x => x.Item)
                .Returns(new TestItem());

            _pageContext
                .SetupGet(x => x.Device)
                .Returns(new Device(Guid.NewGuid()));

            _viewEngine
                .Setup(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_viewEngineResult);

            // Act
            _filter.Process(args);

            // Assert
            Assert.That(_pageContext.Object.PageDefinition.Renderings.Count, Is.EqualTo(1), "Rendering has not been added to the page definition");
            Assert.That(_pageContext.Object.PageDefinition.Renderings[0].Placeholder, Is.EqualTo("main"), "Placeholder name from route not in page definition");
        }

        [Test]
        public void AddContentRenderingToCurrentPageDefinitionByAction()
        {
            // Arrange
            var args = ResultExecutingArgsBuilder(_viewResult);
            args.Context.RouteData.Values["scPlaceholder"] = "main";
            args.Context.RouteData.Values["action"] = "MyView";

            _pageContext
                .SetupGet(x => x.Item)
                .Returns(new TestItem());

            _pageContext
                .SetupGet(x => x.Device)
                .Returns(new Device(Guid.NewGuid()));

            _viewEngine
                .Setup(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_viewEngineResult);

            // Act
            _filter.Process(args);

            // Assert
            Assert.That(_pageContext.Object.PageDefinition.Renderings.Count, Is.EqualTo(1));
        }

        [Test]
        public void InjectorRunsOnceToNotBeReentrant()
        {
            // GitHub Issue #1

            // Arrange
            var args = ResultExecutingArgsBuilder(_viewResult);
            args.Context.RouteData.Values["scPlaceholder"] = "main";
            args.Context.RouteData.Values["action"] = "MyView";

            _pageContext
                .SetupGet(x => x.Item)
                .Returns(new TestItem());

            _pageContext
                .SetupGet(x => x.Device)
                .Returns(new Device(Guid.NewGuid()));

            _viewEngine
                .Setup(x => x.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_viewEngineResult);

            _filter.Process(args);

            // Act
            _filter.Process(args);

            // Assert
            Assert.That(_pageContext.Object.PageDefinition.Renderings.Count, Is.EqualTo(1));
        }


        private ResultExecutingArgs ResultExecutingArgsBuilder(ViewResult viewResult)
        {
            var resultExecutingContext = new ResultExecutingContext(_controller.Object.ControllerContext, viewResult);
            var args = new ResultExecutingArgs(resultExecutingContext);
            return args;


        }
    }
}
