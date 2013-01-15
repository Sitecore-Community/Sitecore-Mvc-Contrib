﻿using System;
using System.IO;
using System.Web.Mvc;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Pipelines.MvcEvents
{
    public class InjectViewInPlaceholderFilter : Mvc.Pipelines.MvcEvents.ResultExecuting.ResultExecutingProcessor
    {
        private readonly ILog _logger;
        private readonly IPageContext _pageContext;

        public InjectViewInPlaceholderFilter(ILog logger, IPageContext pageContext)
        {
            _logger = logger;
            _pageContext = pageContext;
        }

        public InjectViewInPlaceholderFilter()
            : this(new LogWrapper(), new PageContextWrapper(PageContext.Current))
        {

        }

        /// <summary>
        /// Filter for injecting the ViewResult of a controller into a Sitecore placeholder.
        /// 
        /// Use this configuration snippet to enable:
        /// <mvc.resultExecuting>
        ///   <processor type="Sitecore.Mvc.Contrib.Pipelines.MvcEvents.InjectViewInPlaceholderFilter, Sitecore.Mvc.Contrib"/>
        /// </mvc.resultExecuting>
        /// 
        /// Will react to all controllers with the scItemPath and scPlaceholder set in the route data. E.g.:
        /// routes.MapRoute(
        ///   "HelloWorld", // Route name
        ///   "hello/{action}/{id}", // URL with parameters
        ///   new { controller = "HelloWorld", scItemPath = "/sitecore/content/home", scPlaceholder = "main", id = UrlParameter.Optional }
        ///  );
        /// </summary>
        /// <param name="args">Pipeline arguments with filter context</param>
        public override void Process(Mvc.Pipelines.MvcEvents.ResultExecuting.ResultExecutingArgs args)
        {
            _logger.Info("Process called on InjectViewInPlaceholderFilter", this);

            var filterContext = args.Context;
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult == null) return;

            var placeholder = filterContext.RouteData.Values["scPlaceholder"] as string;

            if (IsMissingPresentationValues(placeholder)) return;

            var viewPath = GetViewPath(viewResult, filterContext);      
            var viewEngineResult = GetView(viewResult, filterContext, viewPath);

            viewResult.View = InjectViewInPlace(placeholder, filterContext.Controller.ControllerContext,
                                                viewEngineResult.View,
                                                viewResult.ViewData, viewResult.TempData);
        }

        private static string GetViewPath(ViewResult viewResult, ControllerContext filterContext)
        {
            return (viewResult.ViewName != "")
                                ? viewResult.ViewName
                                : filterContext.Controller.ControllerContext.RouteData.GetRequiredString("action");
        }

        private bool IsMissingPresentationValues(string placeholder)
        {
            return (string.IsNullOrEmpty(placeholder) || _pageContext.Item == null);
        }

        private static ViewEngineResult GetView(ViewResult viewResult, ControllerContext filterContext, string viewPath)
        {
            return IsPartialView(filterContext)
                       ? viewResult.ViewEngineCollection.FindPartialView(filterContext.Controller.ControllerContext,
                                                                         viewPath)
                       : viewResult.ViewEngineCollection.FindView(filterContext.Controller.ControllerContext, viewPath,
                                                                  viewResult.MasterName);
        }

        private static bool IsPartialView(ControllerContext filterContext)
        {
            const bool defaultToTrue = true;
            bool usePartialView;
            var partialView = filterContext.RouteData.Values["partialView"] as string;
            if (string.IsNullOrWhiteSpace(partialView))
            {
                usePartialView = defaultToTrue;
            }
            else
            {
                if (!bool.TryParse(partialView, out usePartialView))
                {
                    usePartialView = defaultToTrue;
                }
            }

            return usePartialView;
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
        protected IView InjectViewInPlace(string placeholder, ControllerContext controllerContext, IView view,
                                          ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            _logger.Info("InjectViewInPlace called on InjectViewInPlaceholderFilter", this);

            if (placeholder != null)
            {
                var contentRendering = new ContentRendering
                                           {
                                               Id = Guid.NewGuid(),
                                               UniqueId = Guid.NewGuid(),
                                               Placeholder = placeholder,
                                               DeviceId = _pageContext.Device.Id
                                           };

                using (var sw = new StringWriter())
                {
                    var viewContext = new ViewContext(controllerContext, view, viewData, tempData, sw);
                    viewContext.View.Render(viewContext, sw);
                    contentRendering.Content = sw.ToString();
                }

                var currentRenderings = _pageContext.PageDefinition.Renderings;
                currentRenderings.Add(contentRendering);
            }

            return _pageContext.PageView;
        }
    }
}