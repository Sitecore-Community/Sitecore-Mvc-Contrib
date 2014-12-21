using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Mvc.Contrib.Pipelines.Response.RenderRendering
{
    public class GenerateCustomCacheKey : GenerateCacheKey
    {
        public override void Process(RenderRenderingArgs args)
        {
            if (args.Rendered || !string.IsNullOrEmpty(args.CacheKey))
                return;

            var cachedRenderingArgs = args as CachedRenderingArgs;
            if (cachedRenderingArgs == null)
                return;

            args.CacheKey = GenerateKey(args.Rendering, cachedRenderingArgs);
            if (!string.IsNullOrEmpty(args.CacheKey))
                args.Cacheable = true;
        }

        protected override string GenerateKey(Rendering rendering, RenderRenderingArgs args)
        {
            var cachedRenderingArgs = args as CachedRenderingArgs;
            if (cachedRenderingArgs == null)
                return string.Empty;
            string str1 = string.IsNullOrEmpty(rendering.Caching.CacheKey)
                ? args.Rendering.Renderer.ValueOrDefault(renderer => renderer.CacheKey)
                : rendering.Caching.CacheKey;
            if (string.IsNullOrEmpty(str1))
                return null;
            string str2 = str1 + "_#lang:" + Globalization.Language.Current.Name.ToUpper();
            if (cachedRenderingArgs.VaryByData)
                str2 = str2 + GetDataPart(rendering);
            if (cachedRenderingArgs.VaryByDevice)
                str2 = str2 + GetDevicePart(rendering);
            if (cachedRenderingArgs.VaryByLogin)
                str2 = str2 + GetLoginPart(rendering);
            if (cachedRenderingArgs.VaryByUser)
                str2 = str2 + GetUserPart(rendering);
            if (cachedRenderingArgs.VaryByParameters)
                str2 = str2 + GetParametersPart(rendering);
            if (cachedRenderingArgs.VaryByQueryString)
                str2 = str2 + GetQueryStringPart(rendering);
            return str2;
        }
    }
}
