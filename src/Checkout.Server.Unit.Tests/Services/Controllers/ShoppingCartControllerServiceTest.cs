using System.Collections.Generic;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Infra.Services.Commands;
using Checkout.Server.Infra.Services.Controllers;
using Checkout.Server.Infra.Services.Queries;
using NSubstitute;
using NUnit.Framework;

namespace Checkout.Server.Unit.Tests.Services.Controllers
{
    [TestFixture]
    public class ShoppingCartControllerServiceTest
    {
        private IShoppingCartControllerService _sut;
        private IShoppingCartQueryService _queryService;
        private IShoppingCartCommandService _commandService;

        [SetUp]
        public void Setup()
        {
            _queryService = Substitute.For<IShoppingCartQueryService>();
            _commandService = Substitute.For<IShoppingCartCommandService>();

            _sut = new ShoppingCartControllerService(_queryService, _commandService);
        }

    }
}
