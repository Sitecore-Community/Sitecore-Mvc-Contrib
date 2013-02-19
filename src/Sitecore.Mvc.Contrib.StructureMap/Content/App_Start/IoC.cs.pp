

using StructureMap;
using sample = Website;

namespace $rootnamespace$.App_Start.SitecoreMvcContrib
{
    public static class IoC
    {
        public static void Configure(IContainer container)
        {
            // Add your registration code here...
            
            container
                .ForGenericType<sample.Interfaces.IFooService>()
                .GetInstanceAs<sample.Services.ConcreteFooService>();
        }
    }
}