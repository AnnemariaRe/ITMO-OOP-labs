using Banks.Accounts;
using Banks.Tools;

namespace Banks.Transactions
{
    public class ReplenishTransaction : Transaction
    {
        public ReplenishTransaction(Account account)
            : base(account)
        {
        }

        public override void Replenish(decimal money)
        {
            if (money <= 0) throw new NullOrEmptyBanksException("Money cannot be null or negative");
            Account.CareTracker.SaveState(new Memento.Memento(Account.Balance));
            Account.Balance += money;
        }
    }
}