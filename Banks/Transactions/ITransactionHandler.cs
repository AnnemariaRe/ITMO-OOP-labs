using Banks.Accounts;
using Banks.Clients;

namespace Banks.Transactions
{
    public interface ITransactionHandler
    {
        ITransactionHandler SetNext(ITransactionHandler handler);
        public void Replenish(decimal sum);
        public void Withdraw(decimal sum);
        public void Transfer(Account replenishAccount, decimal sum);
        public void UndoTransaction();
    }
}