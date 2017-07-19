using System;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;
using Checkout.Server.Infra.Services.Commands;
using NSubstitute;
using NUnit.Framework;

namespace Checkout.Server.Unit.Tests.Services.Commands
{
    [TestFixture]
    public class ShoppingCartCommandServiceTests
    {
        IShoppingCartCommandService _sut;
        private IRepository<IShoppingItemModel> _itemsRepository;
        
        [SetUp]
        public void Setup()
        {
            _itemsRepository = Substitute.For<IRepository<IShoppingItemModel>>();
            _sut = new ShoppingCartCommandService(_itemsRepository);
        }
        
        [Test]
        public void RemoveItem_WhenItemIsNull_ShoulThrowAnIvalidArgumentException()
        {
            //Act
            //Assert
           Assert.Throws<ArgumentException>(() => _sut.RemoveItem(null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void RemoveItem_WhenItemHasNullOrEmptyItemId_ShoulThrowAnIvalidArgumentException(string invalidId)
        {
            //Arrange
            var command = new RemoveItemCommandModel(new ShoppingCartItemModel(invalidId));

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.RemoveItem(command));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void RemoveItem_WhenItemHasQuantityZeroOrNegative_ShoulReturnAnError(int wrongQuantity)
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new ShoppingCartItemModel("WatheverItem", wrongQuantity)));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void RemoveItem_WhenItemDoesntExist_ShoulReturnNotExistingError()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new ShoppingCartItemModel("NoExistingItem")));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void RemoveItem_WhenItemExistsAndTryToDeleteMoreThanExistingQuantity_ShoulReturnNotExistingError()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(new ShoppingCartItemModel("Fanta", 3));

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new ShoppingCartItemModel("Fanta", 4)));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void RemoveItem_WhenItemExistsAndProvidedQuantityIsEqualToTheCurrentOne_ShoulDeleteCompletelyTheItemFromTheCart()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(new ShoppingCartItemModel("Fanta", 3));
            
            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new ShoppingCartItemModel("Fanta", 3)));

            //Assert
            _itemsRepository.Received().Delete(Arg.Any<IComparable>());
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }

        [Test]
        public void RemoveItem_WhenItemExistsAndProvidedQuantityIsLessThanTheCurrentOne_ShoulDeleteTheCorrectItemNumberFromTheCart()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(new ShoppingCartItemModel("Fanta", 3));

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new ShoppingCartItemModel("Fanta", 1)));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<ShoppingCartItemModel>(x => (string) x.Id == "Fanta" && x.Quantity == 2));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }

        [Test]
        public void AddItem_WhenItemIsNull_ShoulThrowAnIvalidArgumentException()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.AddItem(null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void AddItem_WhenItemHasNullOrEmptyItemId_ShoulThrowAnIvalidArgumentException(string invalidId)
        {
            //Arrange
            var command = new AddItemCommandModel(new ShoppingCartItemModel(invalidId));

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.AddItem(command));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void AddItem_WhenItemHasQuantityZeroOrNegative_ShoulReturnAnError(int wrongQuantity)
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.AddItem(new AddItemCommandModel(new ShoppingCartItemModel("WatheverItem", wrongQuantity)));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void AddItem_WhenItemDoesntExist_ShoulAddIt()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.AddItem(new AddItemCommandModel(new ShoppingCartItemModel("NoExistingItem")));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<ShoppingCartItemModel>(x => (string)x.Id == "NoExistingItem" && x.Quantity == 1));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }
        
        [Test]
        public void AddItem_WhenItemExists_ShoulUpdateQuantity()
        {
            //Arrange
            IShoppingItemModel queryResult = new ShoppingCartItemModel("Coca-Cola", 5);
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.AddItem(new AddItemCommandModel(new ShoppingCartItemModel("Coca-Cola", 2)));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<ShoppingCartItemModel>(x => (string)x.Id == "Coca-Cola" && x.Quantity == 7));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }

        [Test]
        public void UpdateItem_WhenItemIsNull_ShoulThrowAnIvalidArgumentException()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.UpdateItem(null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateItem_WhenItemHasNullOrEmptyItemId_ShoulThrowAnIvalidArgumentException(string invalidId)
        {
            //Arrange
            var command = new UpdateItemCommandModel(new ShoppingCartItemModel(invalidId));

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.UpdateItem(command));
        }

        [Test]
        public void AddItem_WhenItemHasQuantityNegative_ShoulReturnAnError()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.UpdateItem(new UpdateItemCommandModel(new ShoppingCartItemModel("WatheverItem", -1)));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void UpdateItem_WhenItemQuantityIsZeroAndNotPresent_ShoulAvoidAnyAction()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.UpdateItem(new UpdateItemCommandModel(new ShoppingCartItemModel("NotExistingItem", 0)));

            //Assert
            _itemsRepository.DidNotReceive().Save(Arg.Any<IShoppingItemModel>());
            _itemsRepository.DidNotReceive().Delete(Arg.Any<IComparable>());
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }

        [Test]
        public void UpdateItem_WhenItemQuantityIsZeroAndAlreadyPresent_ShoulDeleteIt()
        {
            //Arrange
            IShoppingItemModel queryResult = new ShoppingCartItemModel("Pepsi", 3);
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.UpdateItem(new UpdateItemCommandModel(new ShoppingCartItemModel("Pepsi", 0)));

            //Assert
            _itemsRepository.Received().Delete(Arg.Is<IComparable>(x => (string)x == "Pepsi"));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }

        [Test]
        public void UpdateItem_WhenItemQuantityIsPositiveAndAlreadyPresent_ShoulDeleteIt()
        {
            //Arrange
            IShoppingItemModel queryResult = new ShoppingCartItemModel("Pepsi", 3);
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.UpdateItem(new UpdateItemCommandModel(new ShoppingCartItemModel("Pepsi", 2)));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<ShoppingCartItemModel>(x => (string)x.Id == "Pepsi" && x.Quantity == 2));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }
    }
}
