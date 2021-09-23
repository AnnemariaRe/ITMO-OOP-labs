using NUnit.Framework;
using Shops.Services;

namespace Shops.Tests
{
    [TestFixture]
    public class SetAndChangePriceTest
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void SetAndChangePrice()
        {
            // Arrange
            decimal firstPrice = 300;
            decimal expectedPrice = 250;
            var shop = _shopManager.CreateShop("Перекресток", "Богатырский пр, 7к1");
            var product = _shopManager.RegisterProduct("Шоколад Милка");

            // Act
            _shopManager.AddProduct(shop, product, firstPrice, 1);
            _shopManager.ChangePrice(shop, product, expectedPrice);

            // Assert
            Assert.AreEqual(expectedPrice, shop.GetProduct(product.Id).Price);
        }
    }
}