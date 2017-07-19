﻿using System;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;
using Checkout.Server.Infra.Services.Commands;
using NSubstitute;
using NUnit.Framework;

namespace Checkout.Server.Unit.Tests.Services
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
            var command = new RemoveItemCommandModel(new DrinkModel(invalidId));

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.RemoveItem(command));
        }

        [Test]
        public void RemoveItem_WhenItemDoesntExist_ShoulReturnNotExistingError()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new DrinkModel("NoExistingItem")));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void RemoveItem_WhenItemExistsAndTryToDeleteMoreThanExistingQuantity_ShoulReturnNotExistingError()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(new DrinkModel("Fanta", 3));

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new DrinkModel("Fanta", 4)));

            //Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.Error);
            Console.WriteLine(response.Error.Message);
        }

        [Test]
        public void RemoveItem_WhenItemExistsAndProvidedQuantityIsEqualToTheCurrentOne_ShoulDeleteCompletelyTheItemFromTheCart()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(new DrinkModel("Fanta", 3));
            
            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new DrinkModel("Fanta", 3)));

            //Assert
            _itemsRepository.Received().Delete(Arg.Any<IComparable>());
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }

        [Test]
        public void RemoveItem_WhenItemExistsAndProvidedQuantityIsLessThanTheCurrentOne_ShoulDeleteTheCorrectItemNumberFromTheCart()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(new DrinkModel("Fanta", 3));

            //Act
            var response = _sut.RemoveItem(new RemoveItemCommandModel(new DrinkModel("Fanta", 1)));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<DrinkModel>(x => (string) x.Id == "Fanta" && x.Quantity == 2));
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
            var command = new AddItemCommandModel(new DrinkModel(invalidId));

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.AddItem(command));
        }

        [Test]
        public void AddItem_WhenItemDoesntExist_ShoulAddIt()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.AddItem(new AddItemCommandModel(new DrinkModel("NoExistingItem")));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<DrinkModel>(x => (string)x.Id == "NoExistingItem" && x.Quantity == 1));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }
        
        [Test]
        public void AddItem_WhenItemExists_ShoulUpdateQuantity()
        {
            //Arrange
            IShoppingItemModel queryResult = new DrinkModel("Coca-Cola", 5);
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var response = _sut.AddItem(new AddItemCommandModel(new DrinkModel("Coca-Cola", 2)));

            //Assert
            _itemsRepository.Received().Save(Arg.Is<DrinkModel>(x => (string)x.Id == "Coca-Cola" && x.Quantity == 7));
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNull(response.Error);
        }
    }
}