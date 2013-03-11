using Ninject;

using sample = Website;

namespace $rootnamespace$.App_Start.SitecoreMvcContrib
{
    public static class IoC
    {
        public static void Configure(IKernel kernel)
        {
            // Add your registration code here...

            // kernel.Bind<sample.Interfaces.IFooService>()
            //       .To<sample.Services.ConcreteFooService>();
        }
    }
}