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
        [Test]
        public void ExecuteController_UseChildActionBehaviourIsTrue_ControllerContextIsChildActionMustBeTrue()
        {
            var stream = new MemoryStream();
            var reader = new StreamReader(stream);
            var areaData = new AreaRouteData() { Controller="Home", Action="Index", Area="", UseChildActionBehaviour=true };

            var routeData = new RouteData();
            var valuesMock = routeData.Values; //new RouteValueDictionary(); //new Mock<RouteValueDictionary>();
            var dataTokensMock = routeData.DataTokens; //new RouteValueDictionary();//new Mock<RouteValueDictionary>();
            var routeDataMock = new Mock<IRouteData>();
            routeDataMock.Setup(x => x.Values)
                .Returns(valuesMock);
            routeDataMock.Setup(x => x.DataTokens)
                .Returns(dataTokensMock);

            var httpContextMock = HttpContextHelpers.GetMockHttpContext();
            httpContextMock
                .Setup(x => x.Response)
                .Returns(new HttpResponseWrapper(new HttpResponse(new StreamWriter(stream))));
            var requestContext = new RequestContext(httpContextMock.Object, routeData);
            ContextService.Get().Push<PageContext>(new PageContext() { RequestContext = requestContext });

            var pageContextMock = new Mock<IPageContext>();
            pageContextMock
                .Setup(x => x.RequestContext)
                .Returns(requestContext);

            var viewContextProviderMock = new Mock<IViewContextProvider>();
            var runner = new TestAreaControllerRunner(pageContextMock.Object, routeDataMock.Object, viewContextProviderMock.Object, areaData);

            // Act
            runner.TestExecuteController(new HomeController());
            var content = reader.ReadToEnd();

            // Assert
            Assert.That(content == true.ToString());
        }

        public void ExecuteController_ChildActionSettingTrue_ControllerRunnerSetsParentViewContext()
        {

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

        public class HomeController : Controller
        {
            public ActionResult Index()
            {
                return new ContentResult() { Content = this.ControllerContext.IsChildAction.ToString() };
            }
        }
    }
}
