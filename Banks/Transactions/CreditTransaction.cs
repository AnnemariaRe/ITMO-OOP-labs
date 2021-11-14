using Banks.Accounts;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Transactions
{
    public class CreditTransaction : Transaction
    {
        public CreditTransaction(Account account)
            : base(account)
        {
        }

        public override void Withdraw(decimal money)
        {
            if (Account is CreditAccount account)
            {
                if (money <= 0) throw new NullOrEmptyBanksException("Money cannot be null or negative");
                if (!account.IsWithdrawAvaliable(money)) throw new BanksException("Withdrawal is not available");
                account.CareTracker.SaveState(new Memento.Memento(account.Balance));
                account.Balance -= money;
            }
            else
            {
                TransactionHandler?.Withdraw(money);
            }
        }

        public override void Transfer(Account withdrawAccount, decimal money)
        {
            if (Account is CreditAccount account)
            {
                if (money <= 0) throw new NullOrEmptyBanksException("Money cannot be null or negative");
                if (!account.IsWithdrawAvaliable(money)) throw new BanksException("Withdrawal is not available");
                account.CareTracker.SaveState(new Memento.Memento(account.Balance));
                withdrawAccount.CareTracker.SaveState(new Memento.Memento(withdrawAccount.Balance));
                account.Balance -= money;
                withdrawAccount.Balance += money;
            }
            else
            {
                TransactionHandler?.Transfer(withdrawAccount, money);
            }
        }
    }
}