namespace Banks.Memento
{
    public class Memento
    {
        private readonly decimal _balance;

        public Memento(decimal balance)
        {
            _balance = balance;
        }

        public decimal GetState() => _balance;
    }
}