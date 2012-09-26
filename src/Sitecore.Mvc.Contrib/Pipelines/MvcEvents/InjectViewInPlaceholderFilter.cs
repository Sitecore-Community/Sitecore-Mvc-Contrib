using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Pipelines.MvcEvents
{
    public class InjectViewInPlaceholderFilter : Sitecore.Mvc.Pipelines.MvcEvents.ResultExecuting.ResultExecutingProcessor
    {
        /// <summary>
        /// Filter for injecting the ViewResult of a controller into a Sitecore placeholder.
        /// 
        /// Use this configuration snippet to enable:
        /// <mvc.resultExecuting>
        ///   <processor type="Sitecore.Mvc.Contrib.Pipelines.MvcEvents.LegacyInjectFilter, Sitecore.Mvc.Contrib"/>
        /// </mvc.resultExecuting>
        /// 
        /// Will react to all controllers with the scItemPath and scPlaceholder set in the route data. E.g.:
        /// routes.MapRoute(
        ///   "HelloWorld", // Route name
        ///   "hello/{action}/{id}", // URL with parameters
        ///   new { controller = "HelloWorld", scItemPath = "/sitecore/content/home", scPlaceholder = "main", id = UrlParameter.Optional } 
        /// </summary>
        /// <param name="args">Pipeline arguments with filter context</param>
        public override void Process(Sitecore.Mvc.Pipelines.MvcEvents.ResultExecuting.ResultExecutingArgs args)
        {
            var filterContext = args.Context;
            var placeholder = filterContext.RouteData.Values["scPlaceholder"] as string;
            if (!string.IsNullOrEmpty(placeholder) && PageContext.Current.Item != null)
            {
                var res = ((ViewResult)filterContext.Result);
                var razorView = new RazorView(filterContext.Controller.ControllerContext, res.ViewName, "", false, null);
                res.View = InjectViewInPlace(placeholder, filterContext.Controller.ControllerContext, razorView,
                                             res.ViewData, res.TempData);
            }
        }

        /// <summary>
        /// Render a view and add it in placeholder on current page definition.
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="controllerContext"></param>
        /// <param name="view"></param>
        /// <param name="viewData"></param>
        /// <param name="tempData"></param>
        /// <returns></returns>
        protected IView InjectViewInPlace(string placeholder, ControllerContext controllerContext, IView view, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            if (placeholder != null)
            {
                var contentRendering = new ContentRendering()
                {
                    Id = new Guid(),
                    UniqueId = new Guid(),
                    Placeholder = placeholder,
                    DeviceId = PageContext.Current.Device.Id
                };

                var sw = new StringWriter();
                var viewContext = new ViewContext(controllerContext, view, viewData, tempData, sw);
                viewContext.View.Render(viewContext, sw);
                contentRendering.Content = sw.ToString();


                var currentRenderings = PageContext.Current.PageDefinition.Renderings;
                currentRenderings.Add(contentRendering);
            }

            return PageContext.Current.PageView;
        }
    }

}
