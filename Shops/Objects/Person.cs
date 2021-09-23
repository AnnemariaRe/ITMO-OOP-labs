using Shops.Tools;

namespace Shops.Objects
{
    public class Person
    {
        private decimal _balance;
        private string _name;
        public Person(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
        }

        public decimal Balance
        {
            get => _balance;
            set => _balance = value;
        }

        public string Name { get => _name; }

        public void TakeMoney(decimal cash)
        {
            if (cash <= 0) throw new NullException();
            if (cash <= Balance) Balance -= cash;
            else throw new ShopException("Not enough money on balance");
        }
    }
}