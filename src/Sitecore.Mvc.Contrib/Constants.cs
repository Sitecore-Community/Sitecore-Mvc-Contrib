using Sitecore.Data;

namespace Sitecore.Mvc.Contrib
{
    public class Constants
    {
        public class Mvc
        {
            public static string ParentActionViewContext { get { return "ParentActionViewContext"; } }
        }

        public class Fields
        {
            public class Controller
            {
                public static ID Action { get { return new ID("{DED9E431-3604-4921-A4B3-A6EC7636A5B6}"); } }

                public static ID Name { get { return new ID("{E64AD073-DFCC-4D20-8C0B-FE5AA6226CD7}"); } }

                public static ID Area { get { return new ID("{73D7AEB5-49BA-4A4A-A25E-FA20D8391A53}"); } }

                public static ID UseChildActionBehavior { get { return new ID("{7976F4D1-77EC-40EC-AF01-1423E6BE8CF2}"); } }
            }
        }

        public class Templates
        {
            public static ID AreaController { get { return new ID("{16B2D343-B679-47F3-939E-60D184B632A5}"); } }

//            public static ID ControllerRendering { get { return new ID("{2A3E91A0-7987-44B5-AB34-35C2D9DE83B9}"); } }

            public static ID ChildActionRendering { get { return new ID("{E71C57FF-4752-4CB7-811E-489388A3EC9A}"); } }

            public static ID ControllerRenderingWithValidation { get { return new ID("{EF42883B-1D1D-40A6-8FE8-3FB85DE5B73B}"); } }
            
        }
    }
}
