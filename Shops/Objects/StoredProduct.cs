namespace Shops.Objects
{
    public class StoredProduct
    {
        private Product _product;
        private int _quantity;
        private decimal _price;

        public StoredProduct(Product product, int quantity, decimal price)
        {
            _product = product;
            _quantity = quantity;
            _price = price;
        }

        public Product Product
        {
            get => _product;
        }

        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        public decimal Price
        {
            get => _price;
            set => _price = value;
        }

        public void ChangeQuantity(int quantity)
        {
            _quantity = quantity;
        }

        public void ChangePrice(decimal price)
        {
            _price = price;
        }
    }
}