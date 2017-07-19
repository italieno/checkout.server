using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;
using Checkout.Server.Infra.Services.Queries;
using NSubstitute;
using NUnit.Framework;

namespace Checkout.Server.Unit.Tests.Services
{
    [TestFixture]
    public class ShoppingCartQueryServiceTests
    {
        IShoppingCartQueryService _sut;
        private IRepository<IShoppingItemModel> _itemsRepository;
        private readonly IEnumerable<IShoppingItemModel> _emptyList = Enumerable.Empty<IShoppingItemModel>();
        private readonly IEnumerable<IShoppingItemModel> _nullList = null;
        private IEnumerable<IShoppingItemModel> _sampleList;
        private readonly IShoppingItemModel _observedDrink = new DrinkModel("Fanta", 3);

        [SetUp]
        public void Setup()
        {
            _itemsRepository = Substitute.For<IRepository<IShoppingItemModel>>();
            _sut = new ShoppingCartQueryService(_itemsRepository);
        }

        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            _sampleList = new List<IShoppingItemModel>()
            {
                new DrinkModel(id: "Coca-Cola", quantity: 2),
                new DrinkModel(id: "Fanta", quantity: 3),
                new DrinkModel(id: "Sprite"),
                new DrinkModel(id: "7up", quantity: 5),
            };
        }


        [Test]
        public void GetAll_WhenNullDrinks_ShoudlReturnAnEmptyList()
        {
            //Arrange
            _itemsRepository.All().Returns(_nullList);

            //Act
            var result = _sut.GetAll();

            //Assert
            Assert.AreEqual(Enumerable.Empty<IShoppingItemModel>(), result);
        }

        [Test]
        public void GetAll_WhenNoDrinks_ShoudlReturnAnEmptyList()
        {
            //Arrange
            _itemsRepository.All().Returns(_emptyList);

            //Act
            var result = _sut.GetAll();

            //Assert
            Assert.AreEqual(Enumerable.Empty<IShoppingItemModel>(), result);
        }

        [Test]
        public void GetAll_WhenDrinksExists_ShoudlReturnCompleteList()
        {
            //Arrange
            _itemsRepository.All().Returns(_sampleList);

            //Act
            var result = _sut.GetAll();

            //Assert
            Assert.AreEqual(_sampleList.OrderBy(x => x.Id), result.OrderBy(x => x.Id));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetDrinkById_WhenProvidingIvalidParam_ShoudlThrowAnException(string invalidId)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.GetItem(invalidId));
        }
        
        [Test]
        public void GetDrinkById_WhenDrinkDoesNotExist_ShoudlReturnNull()
        {
            //Arrange
            IShoppingItemModel queryResult = null;
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(queryResult);

            //Act
            var result = _sut.GetItem("TheDrinkImLookingFor");

            //Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void GetDrinkById_WhenDrinkExists_ShoudlReturnCorrectItem()
        {
            //Arrange
            _itemsRepository.FindById(Arg.Any<IComparable>()).Returns(_observedDrink);

            //Act
            var result = _sut.GetItem("Fanta");

            //Assert
            Assert.AreEqual(_observedDrink, result);
        }
    }
}
