using NUnit.Framework;
using Shops.Objects;
using Shops.Services;

namespace Shops.Tests
{
    [TestFixture]
    public class AddProductsToShopBuyProductsTest
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }
        
        [Test]
        public void AddProductsToShop_BuyProducts()
        {
            // Arrange
            decimal moneyBefore = 150;
            decimal moneyToBuy = 35;
            int quantityBefore = 8;
            int quantityToBuy = 2;
            
            var customer = new Person("Аннемария", moneyBefore);
            var shop = _shopManager.CreateShop("Пятерочка", "Пионерская ул. 21");
            var product = _shopManager.RegisterProduct("Авокадо");
            var list = new ShoppingList();

            // Act
            list.AddToList(product, quantityToBuy);
            _shopManager.AddProduct(shop, product, moneyToBuy, quantityBefore);
            _shopManager.BuyProducts(shop, customer, list);

            // Assert
            Assert.AreEqual(moneyBefore - moneyToBuy * quantityToBuy, customer.Balance);
            Assert.AreEqual(quantityBefore - quantityToBuy, shop.GetProduct(product.Id).Quantity);
        }
    }
}