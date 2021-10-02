using NUnit.Framework;
using Shops.Objects;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    [TestFixture]
    public class FindShopWithLowestPriceTest
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void ProductsAreAvailable()
        {
            // Arrange
            var honey = _shopManager.RegisterProduct("Алтайский Мед");
            var milk = _shopManager.RegisterProduct("Молоко Простоквашино");
            var water = _shopManager.RegisterProduct("Питьевая вода Bonaqua");

            var shop1 = _shopManager.CreateShop("Пятерочка", "Тележная ул, 22");
            _shopManager.AddProduct(shop1, honey, 229, 10);
            _shopManager.AddProduct(shop1, milk, 187, 10);
            _shopManager.AddProduct(shop1, water, 111, 10);
            
            var shop2 = _shopManager.CreateShop("Пятерочка", "Пр Большевиков, 9");
            _shopManager.AddProduct(shop2, honey, 272, 10);
            _shopManager.AddProduct(shop2, milk, 115, 10);
            _shopManager.AddProduct(shop2, water, 96, 10);
            
            var shop3 = _shopManager.CreateShop("Пятерочка", "Кушелевская дор, 5к1");
            _shopManager.AddProduct(shop3, honey, 341, 10);
            _shopManager.AddProduct(shop3, milk, 153, 10);
            _shopManager.AddProduct(shop3, water, 84, 10);

            // Act
            var result1 = _shopManager.FindCheapestShop(honey, 2);
            var result2 = _shopManager.FindCheapestShop(milk, 4);
            var result3 = _shopManager.FindCheapestShop(water, 7);

            // Assert
            Assert.AreEqual(shop1.Id, result1.Id);
            Assert.AreEqual(shop2.Id, result2.Id);
            Assert.AreEqual(shop3.Id, result3.Id);
        }

        [Test]
        public void NotEnoughProductsInShops()
        {
            Assert.Catch<ShopException>(() =>
            {
                var honey = _shopManager.RegisterProduct("Алтайский Мед");

                var shop1 = _shopManager.CreateShop("Пятерочка", "Тележная ул, 22");
                _shopManager.AddProduct(shop1, honey, 229, 3);

                var shop2 = _shopManager.CreateShop("Пятерочка", "Пр Большевиков, 9");
                _shopManager.AddProduct(shop2, honey, 272, 11);

                var shop3 = _shopManager.CreateShop("Пятерочка", "Кушелевская дор, 5к1");
                _shopManager.AddProduct(shop3, honey, 341, 6);
                
                _shopManager.FindCheapestShop(honey, 15);
            });
        }
        
        [Test]
        public void ProductsAreNotAvailable()
        {
            
            var honey = _shopManager.RegisterProduct("Алтайский Мед");

            var shop1 = _shopManager.CreateShop("Пятерочка", "Тележная ул, 22");
            var shop2 = _shopManager.CreateShop("Пятерочка", "Пр Большевиков, 9");
            var shop3 = _shopManager.CreateShop("Пятерочка", "Кушелевская дор, 5к1");
            
            _shopManager.AddProduct(shop1, honey, 341, 6);
            _shopManager.AddProduct(shop2, honey, 300, 3);
            _shopManager.AddProduct(shop3, honey, 297, 10);
            
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.FindCheapestShop(honey, 15);
            });
        }
    }
}