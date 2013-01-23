using Sitecore.Mvc.Contrib.Controllers;
using Sitecore.Mvc.Controllers;
using Sitecore.StringExtensions;

namespace Sitecore.Mvc.Contrib.Presentation.Renderer
{
    public class AreaControllerRenderer : Mvc.Presentation.Renderer
    {
        private readonly IControllerRunner _controllerRunner;
        private readonly AreaRouteData _areaRouteData;

        public AreaControllerRenderer(IControllerRunner controllerRunner, AreaRouteData areaRouteData)
        {
            _controllerRunner = controllerRunner;
            _areaRouteData = areaRouteData;
        }

        public AreaControllerRenderer(AreaRouteData areaRouteData)
            : this(new AreaControllerRunner(areaRouteData), areaRouteData)
        {
        }

        public override string CacheKey
        {
            get
            {
                return "areacontroller::" + _areaRouteData.Controller + "#" + _areaRouteData.Action + "#" + _areaRouteData.Area;
            }
        }

        public override void Render(System.IO.TextWriter writer)
        {
            var value = _controllerRunner.Execute();
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            writer.Write(value);
        }

        public override string ToString()
        {
            return "Controller: {0}. Action: {1}. Area {2}".FormatWith(new object[]
			{
				_areaRouteData.Controller,
				_areaRouteData.Action,
                _areaRouteData.Area
			});
        }
    }
}
