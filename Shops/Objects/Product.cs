using System;

namespace Shops.Objects
{
    public class Product
    {
        private string _id;
        private string _name;
        public Product(string name)
        {
            _id = Guid.NewGuid().ToString();
            _name = name;
        }

        public string Id { get => _id; }
        public string Name { get => _name; }
    }
}