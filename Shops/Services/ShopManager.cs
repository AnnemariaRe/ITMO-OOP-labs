using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Objects;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Shop> _shops = new ();
        private List<Product> _products = new ();

        public Shop CreateShop(string name, string adress)
        {
            if (name == null || adress == null) throw new NullException();
            var shop = new Shop(name, adress);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            if (name == null) throw new NullException();
            var product = new Product(name);
            _products.Add(product);
            return product;
        }

        public void AddProduct(Shop shop, Product product, decimal price, int quantity)
        {
            if (price <= 0 || quantity <= 0) throw new NullException();
            if (_products.Contains(product))
            {
                _shops.FirstOrDefault(sh => sh.Id == shop.Id)?.AddProduct(product, price, quantity);
            }
            else
            {
                throw new ShopException("Required product is not registered");
            }
        }

        public void ChangePrice(Shop shop, Product product, decimal newPrice)
        {
            if (newPrice <= 0) throw new NullException();

            if (_products.Contains(product) && shop.ContainsProduct(product.Id))
            {
                _shops.FirstOrDefault(sh => sh.Id == shop.Id)?.GetProduct(product.Id).ChangePrice(newPrice);
            }
            else
            {
                throw new ShopException("Required product is not registered or the shop doesn't contain product");
            }
        }

        public Shop FindCheapestShop(Product product, int quantity)
        {
            if (quantity <= 0) throw new NullException();
            decimal minPrice = 0;
            Shop cheapestShop = null;

            foreach (var shop in _shops.Where(shop => shop.ContainsProduct(product.Id))
                .Where(shop => (minPrice == 0 || shop.GetProduct(product.Id).Price < minPrice) &&
                               shop.GetProduct(product.Id).Quantity >= quantity))
            {
                minPrice = shop.GetProduct(product.Id).Price;
                cheapestShop = shop;
            }

            if (cheapestShop == null || minPrice == 0)
                throw new ShopException("Cannot find the cheapest shop: product doesn't exist or not enough product");

            return cheapestShop;
        }

        public decimal BuyProducts(Shop shop, Person customer, ShoppingList products)
        {
            if (shop == null || customer == null || products == null) throw new NullException();
            decimal totalPrice = 0;

            foreach (var product in products.ProductList)
            {
                decimal currentPrice = shop.GetProduct(product.Key.Id).Price * product.Value;

                if (shop.GetProduct(product.Key.Id).Quantity >= product.Value)
                {
                    shop.BuyProduct(product.Key.Id, product.Value);
                    customer.TakeMoney(currentPrice);
                    totalPrice += currentPrice;
                }
                else
                {
                    throw new ShopException("Current shop doesn't have the required quantity of product in the list");
                }

                if (shop.GetProduct(product.Key.Id).Quantity == 0) shop.RemoveProduct(product.Key.Id);
            }

            return totalPrice;
        }

        public void PrintAllShops()
        {
            Console.WriteLine("===SHOPS LIST===");
            foreach (var shop in _shops)
            {
                Console.WriteLine($"Name: {shop.Name},  Adress: {shop.Adress},  Id: {shop.Id}");
            }
        }
    }
}