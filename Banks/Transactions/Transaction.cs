using Banks.Accounts;
using Banks.Clients;
using Banks.Memento;

namespace Banks.Transactions
{
    public class Transaction : ITransactionHandler
    {
        public Transaction(Account account)
        {
            Account = account;
        }

        protected ITransactionHandler TransactionHandler { get; private set; }
        protected Account Account { get; }

        public ITransactionHandler SetNext(ITransactionHandler handler)
        {
            TransactionHandler = handler;
            return handler;
        }

        public virtual void Replenish(decimal money)
        {
            TransactionHandler = new ReplenishTransaction(Account);
            TransactionHandler.Replenish(money);
        }

        public virtual void Withdraw(decimal money)
        {
            TransactionHandler = new DebitTransaction(Account);
            TransactionHandler.SetNext(new DepositTransaction(Account)).SetNext(new CreditTransaction(Account));
            TransactionHandler.Withdraw(money);
        }

        public virtual void Transfer(Account replenishAccount, decimal money)
        {
            TransactionHandler = new DebitTransaction(Account);
            TransactionHandler.SetNext(new DepositTransaction(Account)).SetNext(new CreditTransaction(Account));
            TransactionHandler.Transfer(replenishAccount, money);
        }

        public virtual void UndoTransaction()
        {
            Account.CareTracker.Restore();
        }
    }
}