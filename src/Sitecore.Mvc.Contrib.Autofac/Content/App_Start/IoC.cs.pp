using Autofac;
using Autofac.Integration.Mvc;

using sample = Website;

namespace $rootnamespace$.App_Start
{
    public static class IoC
    {
        public static void Configure(ContainerBuilder builder)
        {
            // Add your registration code here...

            // builder.RegisterType<sample.Services.ConcreteFooService>()
            //        .As<sample.Interfaces.IFooService>()
            //        .InstancePerHttpRequest();
        }
    }
}