using System.Web.Routing;

namespace Sitecore.Mvc.Contrib.Controllers
{
    public interface IRegisterRoutes
    {
        void InstallRoutes(RouteCollection routes);
    }
}