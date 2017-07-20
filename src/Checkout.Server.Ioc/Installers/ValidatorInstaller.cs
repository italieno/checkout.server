using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Checkout.Server.Infra.Helpers;
using FluentValidation;

namespace Checkout.Server.Ioc.Installers
{
    public class ValidatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = Classes.FromAssemblyInDirectory(new AssemblyFilter(AssemblyHelper.AssemblyDirectory));

            container.Register(
                assemblies
                    .BasedOn(typeof(AbstractValidator<>))
                    .WithServiceFirstInterface()
                    .LifestyleSingleton());

        }
    }
}
