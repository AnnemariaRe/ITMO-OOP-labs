using NUnit.Framework;
using Shops.Objects;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    [TestFixture]
    public class BuyManyProductsTest
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void BuyProductsAtPossiblePrice()
        {
            // Arrange
            decimal expectedTotalPrice = 3539;
            int expectedTotalAmount = 73;
            
            var customer = new Person("Аннемария", 5000);
            var shop = _shopManager.CreateShop("Перекресток", "Богатырский пр, 7к1");
            var product1 = _shopManager.RegisterProduct("Сыр Ламбер");
            var product2 = _shopManager.RegisterProduct("Креветки королевские");
            var product3 = _shopManager.RegisterProduct("Чай черный");
            var product4 = _shopManager.RegisterProduct("Напиток Pepsi");
            
            _shopManager.AddProduct(shop, product1, 69, 78);
            _shopManager.AddProduct(shop, product2, 410, 100);
            _shopManager.AddProduct(shop, product3, 229, 34);
            _shopManager.AddProduct(shop, product4, 84, 20);
            
            var list = new ShoppingList();
            list.AddToList(product1, 5);
            list.AddToList(product2, 2);
            list.AddToList(product3, 10);
            list.AddToList(product4, 1);
            
            // Act
            var totalPrice = _shopManager.BuyProducts(shop, customer, list);
            var productAmount = shop.GetProduct(product1.Id).Quantity;

            // Assert
            Assert.AreEqual(expectedTotalPrice, totalPrice);
            Assert.AreEqual(expectedTotalAmount, productAmount);
        }

        [Test]
        public void NotEnoughProducts()
        {
            var customer = new Person("Аннемария", 5000);
            var shop = _shopManager.CreateShop("Перекресток", "Богатырский пр, 7к1");
            var product1 = _shopManager.RegisterProduct("Сыр Ламбер");
            var product2 = _shopManager.RegisterProduct("Креветки королевские");

            _shopManager.AddProduct(shop, product1, 69, 4);
            _shopManager.AddProduct(shop, product2, 410, 1);

            var list = new ShoppingList();
            list.AddToList(product1, 10);
            list.AddToList(product2, 3);
            
            Assert.Catch<ShopException>( () =>
            {
                _shopManager.BuyProducts(shop, customer, list);
            });
        }

        [Test]
        public void NotEnoughMoney()
        {
            var customer = new Person("Аннемария", 1000);
            var shop = _shopManager.CreateShop("Перекресток", "Богатырский пр, 7к1");
            var product1 = _shopManager.RegisterProduct("Сыр Ламбер");
            var product2 = _shopManager.RegisterProduct("Креветки королевские");

            _shopManager.AddProduct(shop, product1, 69, 40);
            _shopManager.AddProduct(shop, product2, 410, 100);

            var list = new ShoppingList();
            list.AddToList(product1, 10);
            list.AddToList(product2, 3);
            
            Assert.Catch<ShopException>( () =>
            {
                _shopManager.BuyProducts(shop, customer, list);
            });
        }
    }
}