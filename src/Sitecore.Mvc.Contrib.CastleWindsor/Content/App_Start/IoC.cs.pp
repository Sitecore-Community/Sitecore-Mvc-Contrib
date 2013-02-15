using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace $rootnamespace$.App_Start.SitecoreMvcContrib
{
    public class IoC
    {
        public static void Configure(WindsorContainer container)
        {
            // Add your registration code here...

               container.Register(
                            Component.For<Website.Interfaces.IFooService>().ImplementedBy<Website.Services.ConcreteFooService>().LifestyleTransient()
                            );
        }
    }
}