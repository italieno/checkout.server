using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Server.Core.Models.Api.Inputs;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Infra.Services.Commands;
using Checkout.Server.Infra.Services.Controllers;
using Checkout.Server.Infra.Services.Queries;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Checkout.Server.Unit.Tests.Services.Controllers
{
    [TestFixture]
    public class ShoppingCartControllerServiceTest
    {
        private IShoppingCartControllerService _sut;
        private IShoppingCartQueryService _queryService;
        private IShoppingCartCommandService _commandService;
        private readonly IEnumerable<IShoppingItemModel> _nullList = null;
        private IEnumerable<IShoppingItemModel> _sampleList;

        [SetUp]
        public void Setup()
        {
            _queryService = Substitute.For<IShoppingCartQueryService>();
            _commandService = Substitute.For<IShoppingCartCommandService>();

            _sut = new ShoppingCartControllerService(_queryService, _commandService);
        }

        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            _sampleList = new List<IShoppingItemModel>()
            {
                new ShoppingCartItemModel(id: "Coca-Cola", quantity: 2),
                new ShoppingCartItemModel(id: "Fanta", quantity: 3),
                new ShoppingCartItemModel(id: "Sprite"),
                new ShoppingCartItemModel(id: "7up", quantity: 5),
            };
        }

        [Test]
        public void GetAll_WhenAllGood_ShouldReturnSuccessResponse()
        {
            //Arrange
            _queryService.GetAll().Returns(_sampleList);

            //Act
            var response = _sut.GetAll();

            //Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Content);
        }

        [Test]
        public void GetAll_WhenThrowAnException_ShouldReturnErrorResponse()
        {
            //Arrange
            _queryService.GetAll().Throws(new Exception());

            //Act
            var response = _sut.GetAll();

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.ApiError);
        }

        [Test]
        public void GetItem_WhenAllGood_ShouldReturnSuccessResponse()
        {
            //Arrange
            _queryService.GetItem(Arg.Any<string>()).Returns(new ShoppingCartItemModel("SomeRandomItem", 13));

            //Act
            var response = _sut.GetItem(Arg.Any<string>());

            //Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Content);
        }

        [Test]
        public void GetItem_WhenThrowAnException_ShouldReturnErrorResponse()
        {
            //Arrange
            _queryService.GetItem(Arg.Any<string>()).Throws(new Exception());

            //Act
            var response = _sut.GetItem(Arg.Any<string>());

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.ApiError);
        }

        [Test]
        public void RemoveItem_WhenAllGood_ShouldReturnSuccessResponse()
        {
            //Arrange
            _commandService.RemoveItem(Arg.Any<RemoveItemCommandModel>()).Returns(new SuccessCommandResponseModel(true));

            //Act
            var response = _sut.RemoveItem(new ShoppingCartItemInputModel()
            {
                HowMany = 1,
                What = "Something"
            });

            //Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Content);
        }
        
        [Test]
        public void RemoveItem_WhenThrowAnException_ShouldReturnErrorResponse()
        {
            //Arrange
            _commandService.RemoveItem(Arg.Any<RemoveItemCommandModel>()).Throws(new Exception());

            //Act
            var response = _sut.RemoveItem(new ShoppingCartItemInputModel()
            {
                HowMany = 1,
                What = "Something"
            });

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.ApiError);
        }

        [Test]
        public void AddItem_WhenAllGood_ShouldReturnSuccessResponse()
        {
            //Arrange
            _commandService.AddItem(Arg.Any<AddItemCommandModel>()).Returns(new SuccessCommandResponseModel(true));

            //Act
            var response = _sut.AddItem(new ShoppingCartItemInputModel()
            {
                HowMany = 2,
                What = "SomethingElse"
            });

            //Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Content);
        }

        [Test]
        public void AddItem_WhenThrowAnException_ShouldReturnErrorResponse()
        {
            //Arrange
            _commandService.AddItem(Arg.Any<AddItemCommandModel>()).Throws(new Exception());

            //Act
            var response = _sut.AddItem(new ShoppingCartItemInputModel()
            {
                HowMany = 2,
                What = "SomethingElse"
            });

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.ApiError);
        }

        [Test]
        public void UpdateItem_WhenAllGood_ShouldReturnSuccessResponse()
        {
            //Arrange
            _commandService.UpdateItem(Arg.Any<UpdateItemCommandModel>()).Returns(new SuccessCommandResponseModel(true));

            //Act
            var response = _sut.UpdateItem(new ShoppingCartItemInputModel()
            {
                HowMany = 3,
                What = "Stuff"
            });

            //Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Content);
        }

        [Test]
        public void UpdateItem_WhenThrowAnException_ShouldReturnErrorResponse()
        {
            //Arrange
            _commandService.UpdateItem(Arg.Any<UpdateItemCommandModel>()).Throws(new Exception());

            //Act
            var response = _sut.UpdateItem(new ShoppingCartItemInputModel()
            {
                HowMany = 3,
                What = "Stuff"
            });

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.ApiError);
        }

    }
}
