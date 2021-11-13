using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Memento
{
    public class CareTracker
    {
        private readonly Account _account;
        private readonly List<Memento> _balanceStates = new ();

        public CareTracker(Account account)
        {
            _account = account;
        }

        public IReadOnlyList<Memento> BalanceStates => _balanceStates;

        public void SaveState(Memento balance)
        {
            _balanceStates.Add(balance);
        }

        public Memento Restore()
        {
            var result = _balanceStates.LastOrDefault() ??
                   throw new BanksException("Cannot restore the state. Collection is empty :(");
            _account.Restore(result);
            _balanceStates.Remove(result);
            return result;
        }
    }
}