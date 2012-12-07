using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Sitecore.Mvc.Contrib.Html
{
    //ME - Copied from System.Web.Mvc.Html.LabelExtensions
   public static class ScLabelExtensions
    {
        public static MvcHtmlString ScLabel(this HtmlHelper html, string expression)
        {
            
            return ScLabelExtensions.ScLabel(html, expression, (string)null);
        }

        public static MvcHtmlString ScLabel(this HtmlHelper html, string expression, string ScLabelText)
        {
            //ME - we have to wrap this section in the EditMode so that the ResourceSet knows to use the field render.
            using (new EditMode())
            {
                return ScLabelExtensions.ScLabelHelper(html,
                                                       ModelMetadata.FromStringExpression(expression, html.ViewData),
                                                       expression, ScLabelText);
            }
        }

        public static MvcHtmlString ScLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return ScLabelExtensions.ScLabelFor<TModel, TValue>(html, expression, (string)null);
        }

        public static MvcHtmlString ScLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string ScLabelText)
        {
            //ME - we have to wrap this section in the EditMode so that the ResourceSet knows to use the field render.
            using (new EditMode())
            {
                return ScLabelExtensions.ScLabelHelper((HtmlHelper) html,
                                                       ModelMetadata.FromLambdaExpression<TModel, TValue>(expression,html.ViewData),
                                                       ExpressionHelper.GetExpressionText((LambdaExpression) expression),
                                                       ScLabelText);
            }
        }

        public static MvcHtmlString ScLabelForModel(this HtmlHelper html)
        {
            return ScLabelExtensions.ScLabelForModel(html, (string)null);
        }

        public static MvcHtmlString ScLabelForModel(this HtmlHelper html, string ScLabelText)
        {
            return ScLabelExtensions.ScLabelHelper(html, html.ViewData.ModelMetadata, string.Empty, ScLabelText);
        }

        internal static MvcHtmlString ScLabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string ScLabelText = null)
        {
            string str = ScLabelText;
            if (str == null)
            {
                string displayName = metadata.DisplayName;
                if (displayName == null)
                {
                    string propertyName = metadata.PropertyName;
                    if (propertyName == null)
                        str = Enumerable.Last<string>((IEnumerable<string>)htmlFieldName.Split(new char[1]
            {
              '.'
            }));
                    else
                        str = propertyName;
                }
                else
                    str = displayName;
            }
            string innerText = str;
            if (string.IsNullOrEmpty(innerText))
                return MvcHtmlString.Empty;
            System.Web.Mvc.TagBuilder tagBuilder = new TagBuilder("ScLabel");
            tagBuilder.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));

            //ME - This line has to be changed
            tagBuilder.InnerHtml = innerText;

            return new MvcHtmlString(tagBuilder.ToString( TagRenderMode.Normal));
        }
    }
}
