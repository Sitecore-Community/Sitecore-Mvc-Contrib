using NUnit.Framework;
using Sitecore.Mvc.Contrib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Sitecore.Mvc.Presentation;
using System.Web.Routing;
using Sitecore.Mvc.Common;
using System.Web.Mvc;
using System.Web.Mvc.Test;
using System.Web;
using System.IO;

namespace Sitecore.Mvc.Contrib.Test.Controllers
{
    [TestFixture]
    public class AreaControllerRunnerTests
    {
        RouteData routeData = null;
        Mock<HttpContextBase> httpContextMock = null;
        Mock<IPageContext> pageContextMock = null;
        Mock<IRouteData> routeDataMock = null;
        Mock<IViewContextProvider> viewContextProviderMock = null;
        ControllerMock triangulationController = new ControllerMock();
        AreaRouteData areaData = null;

        public void Setup()
        {
            areaData = new AreaRouteData()
            {
                Controller = "Home",
                Action = "Index",
                Area = "Temp",
                UseChildActionBehavior = true
            };

            var stream = new MemoryStream();

            routeData = new RouteData();
            routeDataMock = new Mock<IRouteData>();
            routeDataMock.Setup(x => x.Values)
                .Returns(routeData.Values);
            routeDataMock.Setup(x => x.DataTokens)
                .Returns(routeData.DataTokens);

            httpContextMock = HttpContextHelpers.GetMockHttpContext();
            httpContextMock
                .Setup(x => x.Response)
                .Returns(new HttpResponseWrapper(new HttpResponse(new StreamWriter(stream))));
            var requestContext = new RequestContext(httpContextMock.Object, routeData);
            ContextService.Get().Push<PageContext>(new PageContext() { RequestContext = requestContext });

            pageContextMock = new Mock<IPageContext>();
            pageContextMock
                .Setup(x => x.RequestContext)
                .Returns(requestContext);

            viewContextProviderMock = new Mock<IViewContextProvider>();
        }


        [Test]
        public void ExecuteController_Sets_ParentActionViewContext_When_UseChildActionBehavior_Is_True()
        {
            // Arrange
            Setup();
            var sut = new TestAreaControllerRunner(pageContextMock.Object, routeDataMock.Object, viewContextProviderMock.Object, areaData);

            // Act
            sut.TestExecuteController(triangulationController);

            // Assert
            Assert.IsTrue(triangulationController.IsChildAction);
        }

        [Test]
        public void ExecuteController_Sets_ParentActionViewContext_When_UseChildActionBehavior_Is_False()
        {
            // Arrange
            Setup();
            areaData.UseChildActionBehavior = false;
            var sut = new TestAreaControllerRunner(pageContextMock.Object, routeDataMock.Object, viewContextProviderMock.Object, areaData);

            // Act
            sut.TestExecuteController(triangulationController);

            // Assert
            Assert.IsFalse(triangulationController.IsChildAction);
        }


        [Test]
        public void ExecuteController_Removes_ParentActionViewContext_From_DataTokens_Before_Returning_When_ParentActionViewContext_Is_Null()
        {
            // Arrange
            Setup();
            var sut = new TestAreaControllerRunner(pageContextMock.Object, routeDataMock.Object, viewContextProviderMock.Object, areaData);

            // Act
            sut.TestExecuteController(triangulationController);

            // Assert
            Assert.IsFalse(routeData.DataTokens.ContainsKey(Constants.Mvc.ParentActionViewContext));
        }

        [Test]
        public void ExecuteController_Reinstantes_ParentActionViewContext_In_DataTokens_Before_Returning_When_ParentActionViewContext_Is_Not_Null()
        {
            // Arrange
            Setup();
            var providedViewContext = new ViewContext();
            var parentViewContext = new ViewContext();
            routeData.DataTokens.Add(Constants.Mvc.ParentActionViewContext, parentViewContext);
            viewContextProviderMock
                .Setup(p => p.GetCurrentViewContext())
                .Returns(providedViewContext);
            var sut = new TestAreaControllerRunner(pageContextMock.Object, routeDataMock.Object, viewContextProviderMock.Object, areaData);

            // Act
            sut.TestExecuteController(triangulationController);

            // Assert
            Assert.AreSame(routeData.DataTokens[Constants.Mvc.ParentActionViewContext], parentViewContext);
        }

        [Test]
        public void ExecuteController_Uses__ViewContext_From_Provider_When_Setting_Up_ParentActionViewContext()
        {
            // Arrange
            Setup();
            var providedViewContext = new ViewContext();
            var parentViewContext = new ViewContext();
            routeData.DataTokens.Add(Constants.Mvc.ParentActionViewContext, parentViewContext);
            viewContextProviderMock
                .Setup(p => p.GetCurrentViewContext())
                .Returns(providedViewContext);
            var sut = new TestAreaControllerRunner(pageContextMock.Object, routeDataMock.Object, viewContextProviderMock.Object, areaData);

            // Act
            sut.TestExecuteController(triangulationController);

            // Assert
            Assert.AreSame(triangulationController.ParentActionViewContext, providedViewContext);
        }

        public class TestAreaControllerRunner : AreaControllerRunner
        {
            public TestAreaControllerRunner(IPageContext pageContext, IRouteData routeData, IViewContextProvider viewContextProvider, AreaRouteData areaRouteData)
                : base(pageContext, routeData, viewContextProvider, areaRouteData)
            {
            }

            public void TestExecuteController(Controller controller)
            {
                ExecuteController(controller);
            }
        }

        public class ControllerMock : Controller
        {
            public bool IsChildAction { get; set; }
            public ViewContext ParentActionViewContext { get; set; }

            protected override void Execute(RequestContext requestContext)
            {
                IsChildAction = requestContext.RouteData.DataTokens.ContainsKey(Constants.Mvc.ParentActionViewContext);
                ParentActionViewContext = requestContext.RouteData.DataTokens[Constants.Mvc.ParentActionViewContext] as ViewContext;
            }
        }
    }
}
