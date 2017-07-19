using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;
using Checkout.Server.Dal.Stores;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Checkout.Server.Unit.Tests.Repositories
{
    [TestFixture]
    public class SimpleInMemoryShoppingItemsRepositoryTests
    {
        private IRepository<IShoppingItemModel> _sut;
        private ISimpleShoppingItemStore _storeMock;
        private static readonly string ObservedDrink = "Fanta";
        private readonly IEnumerable<IShoppingItemModel> _emptyList = Enumerable.Empty<IShoppingItemModel>();
        private readonly IEnumerable<IShoppingItemModel> _nullList = null;
        private IEnumerable<IShoppingItemModel> _sampleList;


        [SetUp]
        public void Setup()
        {
            _storeMock = Substitute.For<ISimpleShoppingItemStore>();
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
        public void NewInstanceOfRepository_WhenStoreIsNotProvided_ShoulThrowAnIvalidArgumentException()
        {
            //Arrange, Act and Assert                           
            Assert.Throws<ArgumentException>(() => { _sut = new SimpleInMemoryShoppingItemsRepository(null); });
        }

        [Test]
        public void GetAll_WhenStoreIsEmpty_ShoulReturnEmptyDataList()
        {
            //Arrange
            _storeMock.LoadData().Returns(_emptyList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            var result = _sut.All();

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenStoreIsNull_ShoulReturnEmptyList()
        {
            //Arrange
            _storeMock.LoadData().Returns(_nullList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);
            
            //Act
            var result = _sut.All();

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenStorePopulated_ShoulReturnCompleteList()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            var result = _sut.All();

            //Assert
            Assert.AreEqual(_sampleList.OrderBy(x => x.Id), result.OrderBy(x => x.Id));
        }

        [Test]
        public void FindById_WhenNullComparableIsProvided_ShoulThrowAnIvalidArgumentException()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act and Assert
            Assert.Throws<ArgumentException>(() => { _sut.FindById(null); });
        }

        [Test]
        public void FindById_WhenNotFindAny_ShoulReturnNull()
        {
            //Arrange
            _storeMock.LoadData().Returns(_emptyList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            var result = _sut.FindById(Guid.NewGuid());

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void FindById_WhenFindOne_ShoulReturnCorrectItem()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            var result = _sut.FindById(ObservedDrink);

            //Assert
            Assert.AreEqual(ObservedDrink, result.Id);
        }

        [Test]
        public void Save_WhenItemProvidedIsNull_ShoulThrowAnIvalidArgumentException()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act and Assert
            Assert.Throws<ArgumentException>(() => { _sut.Save(null); });
        }

        [Test]
        public void Save_WhenItemDoesNotExist_ShoulAddNewItem()
        {
            //Arrange
            _storeMock.LoadData().Returns(_emptyList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            _sut.Save(new DrinkModel(id: "AnotherRandomDrink", quantity: 2));
            var result = _sut.All();

            //Assert
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void Save_AddingTwiceSameId_ShoulAddAndThenUpdate()
        {
            //Arrange
            _storeMock.LoadData().Returns(_emptyList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            var drink = new DrinkModel(id: "RandomDrink", quantity: 2);
            _sut.Save(drink);
            var result = _sut.All();

            //Assert
            Assert.IsTrue(result.Any());

            //Act [Again]
            var drinkWithTheSameName = new DrinkModel(id: "RandomDrink", quantity: 3);
            _sut.Save(drinkWithTheSameName);
            result = _sut.All();

            //Assert [Again]
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.FirstOrDefault().Quantity == 3);
        }

        [Test]
        public void Save_WhenItemExists_ShoulUpdateIt()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            var observedItem = _sut.FindById(ObservedDrink);
            
            _sut.Save(new DrinkModel(observedItem.Id.ToString(), 8));
            var result = _sut.FindById(ObservedDrink);

            //Assert
            Assert.AreEqual(_sampleList.Count(), _sut.All().Count());
            Assert.IsTrue(result.Quantity == 8);
        }

        [Test]
        public void Delete_WheItemProvidedIsNull_ShoulThrowAnIvalidArgumentException()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act and Assert
            Assert.Throws<ArgumentException>(() => { _sut.Delete(null); });
        }

        [Test]
        public void Delete_WhenItemDoesntExist_ShoulThrowAKeyNotFoundException()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act and Assert
            Assert.Throws<KeyNotFoundException>(() => { _sut.Delete("NotExistingDrink"); });

        }

        [Test]
        public void Delete_WhenItemExists_ShoulDeleteTheItem()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            _sut.Delete(ObservedDrink);
            var result = _sut.FindById(ObservedDrink);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        [Ignore("I kept this test just to proof that SimpleInMemoryShoppingItemsRepository is not thread safe")]
        public void Save_WhenMultipleThreadRaceForTheSameResource_ShouldHandleOptimisticConcurrency()
        {
            //Arrange
            _storeMock.LoadData().Returns(_sampleList);
            _sut = new SimpleInMemoryShoppingItemsRepository(_storeMock);

            //Act
            Thread t1 = new Thread(() =>
            {
                try
                {
                    var thread1Drink = _sut.FindById(ObservedDrink);
                    _sut.Save(new DrinkModel(id: thread1Drink.Id.ToString(), quantity: 10));
                    Console.WriteLine($"t1. Drink Inserted [{10}]");
                }
                catch (Exception e)
                {
                    Console.WriteLine("t1. exception: " + e.Message);
                }
            });

            Thread t2 = new Thread(() =>
            {
                try
                {
                    var thread2Drink = _sut.FindById(ObservedDrink);
                  
                    //simulate some heavy work here to make sure the write has been performed after t1 avoiding random result
                    Thread.Sleep(2000);

                    _sut.Save(new DrinkModel(id: thread2Drink.Id.ToString(), quantity: 12));
                    Console.WriteLine($"t1. Drink Inserted [{12}]");

                }
                catch (Exception e)
                {
                    Console.WriteLine("t2. exception: " + e.Message);
                }

            });

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            var mainThreadDrink = _sut.FindById(ObservedDrink);
            Console.WriteLine(string.Format($"m1. Drink Inserted [{mainThreadDrink.Quantity}]"));

            //Assert
            Assert.AreEqual(12, mainThreadDrink.Quantity);
        }
    }
}