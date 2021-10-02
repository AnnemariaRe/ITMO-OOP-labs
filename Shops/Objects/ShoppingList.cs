using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Objects
{
    public class ShoppingList
    {
        private Dictionary<Product, int> _productList;
        public ShoppingList() => _productList = new Dictionary<Product, int>();

        public Dictionary<Product, int> ProductList { get => _productList; }

        public void AddToList(Product product, int quantity)
        {
            if (product == null || quantity <= 0) throw new NullException();
            ProductList.Add(product, quantity);
        }
    }
}