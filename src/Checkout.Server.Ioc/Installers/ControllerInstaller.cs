using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Checkout.Server.Ioc.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = Classes.FromAssemblyInDirectory(new AssemblyFilter(Checkout.Server.Infra.Helpers.AssemblyHelper.AssemblyDirectory));

            container.Register(
                assemblies
                    .BasedOn<ApiController>()
                    .WithServiceSelf()
                    .LifestylePerWebRequest());
        }
    }
}