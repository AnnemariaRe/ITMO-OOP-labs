using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Objects
{
    public class Shop
    {
        private string _id;
        private string _name;
        private string _adress;

        public Shop(string name, string adress)
        {
            _name = name;
            _adress = adress;
            ProductList = new Dictionary<string, StoredProduct>();
            _id = Guid.NewGuid().ToString();
        }

        public string Id { get => _id; }
        public string Name { get => _name; }
        public string Adress { get => _adress; }
        private Dictionary<string, StoredProduct> ProductList { get; }

        public void AddProduct(Product product, decimal price, int quantity)
        {
            if (ProductList.ContainsKey(product.Id))
            {
                ProductList[product.Id].Price += price;
                ProductList[product.Id].Quantity += quantity;
            }
            else
            {
                ProductList.Add(product.Id, new StoredProduct(product, quantity, price));
            }
        }

        public StoredProduct GetProduct(string id)
        {
            if (ProductList.ContainsKey(id)) return ProductList[id];
            throw new ShopException("Product with the current id doesn't exist");
        }

        public bool ContainsProduct(string id)
        {
            return ProductList.ContainsKey(id);
        }

        public void RemoveProduct(string id)
        {
            if (ProductList.ContainsKey(id)) ProductList.Remove(id);
        }

        public void BuyProduct(string id, int quantity)
        {
            if (ProductList.ContainsKey(id))
            {
                if (ProductList[id].Quantity >= quantity)
                {
                    ProductList[id].ChangeQuantity(ProductList[id].Quantity - quantity);
                }
            }
        }

        public void ProductPrinter()
        {
            Console.WriteLine($"All products from {Name} ({Adress}):");

            foreach (var product in ProductList)
            {
                Console.WriteLine($"name: {product.Value.Product.Name},  price: {product.Value.Price} rub,  amount: {product.Value.Quantity}");
            }
        }
    }
}