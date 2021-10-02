using System;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = new ShopManager();
            var product1 = shopManager.RegisterProduct("Яблоко");
            var product2 = shopManager.RegisterProduct("Пельмени");
            var product3 = shopManager.RegisterProduct("Апельсиновый сок");
            var product4 = shopManager.RegisterProduct("Паштет");
            var product5 = shopManager.RegisterProduct("Картофель");

            var shop1 = shopManager.CreateShop("Пятерочка", "Тележная ул, 22");
            var shop2 = shopManager.CreateShop("Перекресток", "Уточкина ул, 4к1А");
            var shop3 = shopManager.CreateShop("Дикси", "Карбышева ул, 9а");

            shopManager.AddProduct(shop1, product1, 68, 123);
            shopManager.AddProduct(shop1, product2, 229, 30);
            shopManager.AddProduct(shop1, product5, 71, 248);

            shopManager.AddProduct(shop2, product3, 152, 100);
            shopManager.AddProduct(shop2, product4, 311, 150);

            shopManager.AddProduct(shop3, product1, 57, 90);
            shopManager.AddProduct(shop3, product2, 200, 120);
            shopManager.AddProduct(shop3, product3, 133, 200);
            shopManager.AddProduct(shop3, product4, 299, 190);
            shopManager.AddProduct(shop3, product5, 59, 322);

            shopManager.PrintAllShops();
            shop1.ProductPrinter();
            shop2.ProductPrinter();
            shop3.ProductPrinter();
        }
    }
}
