using System;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Checkout.Server.Ioc.Registrars
{
    public class WindsorContainerConfigurator
    {
        public IWindsorContainer Configure(HttpConfiguration config)
        {
            var location = AppDomain.CurrentDomain.BaseDirectory.Contains("bin")
               ? AppDomain.CurrentDomain.BaseDirectory
               : $"{AppDomain.CurrentDomain.BaseDirectory}bin";
            
            var assembly = new AssemblyFilter(location);

            var container = new WindsorContainer()
                .Install(FromAssembly.InDirectory(assembly));

            config.DependencyResolver = new WindsorResolver(container);
            return container;
        }
    }
}