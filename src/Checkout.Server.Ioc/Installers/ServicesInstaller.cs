using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;
using Checkout.Server.Dal.Stores;
using Checkout.Server.Infra.Services.Commands;
using Checkout.Server.Infra.Services.Controllers;
using Checkout.Server.Infra.Services.Queries;

namespace Checkout.Server.Ioc.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISimpleShoppingItemStore>().ImplementedBy<EmptyShoppingItemStore>(),
                Component.For<IRepository<IShoppingItemModel>>().ImplementedBy<SimpleInMemoryShoppingItemsRepository>(),
                Component.For<IShoppingCartQueryService>().ImplementedBy<ShoppingCartQueryService>(),
                Component.For<IShoppingCartCommandService>().ImplementedBy<ShoppingCartCommandService>(),
                Component.For<IShoppingCartControllerService>().ImplementedBy<ShoppingCartControllerService>()
              );
        }
    }
}
