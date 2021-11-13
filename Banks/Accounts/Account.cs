using System;
using Banks.Clients;
using Banks.Memento;
using Banks.Tools;

namespace Banks.Accounts
{
    public abstract class Account
    {
        private const int TotalDaysInMonth = 30;
        protected Account(decimal balance, Client owner)
        {
            if (balance < 0) throw new NullOrEmptyBanksException("Balance cannot be negative");
            AccountType = null;
            Balance = balance;
            Id = Guid.NewGuid();
            Owner = owner ?? throw new NullOrEmptyBanksException("Client cannot be null");
            LastInterest = DateTime.Now;
            Profit = 0;
            InterestOnBalance = 0;
            CareTracker = new CareTracker(this);
        }

        public decimal Balance { get; internal set; }
        public string AccountType { get; internal set; }
        public Guid Id { get; }
        public Client Owner { get; }
        public decimal Profit { get; protected set; }
        public decimal InterestOnBalance { get; protected set; }
        public DateTime FirstInterest { get; protected set; }
        public DateTime LastInterest { get; protected set; }
        public int Limit { get; protected set; }
        public decimal Comission { get; protected set; }
        public DateTime ExpirationDate { get; protected set; }
        public CareTracker CareTracker { get; }

        public void CalculateDayProfit()
        {
            if (InterestOnBalance <= 0) throw new BanksException("Cannot calculate day profit");
            if (Profit == 0) FirstInterest = DateTime.Today;
            if (LastInterest == DateTime.Today) throw new BanksException("The profit has already counted today");
            Profit += Balance * (InterestOnBalance / 365) / 100;
            LastInterest = DateTime.Today;
        }

        public bool UpdateBalance()
        {
            TimeSpan duration = LastInterest - FirstInterest;
            if (duration.Days == TotalDaysInMonth)
            {
                Balance += Profit;
                Profit = 0;
                return true;
            }

            return false;
        }

        public void Restore(Memento.Memento memento)
        {
            Balance = memento.GetState();
        }

        public abstract bool IsWithdrawAvaliable(decimal value);

        public void PrintInfo()
        {
            switch (this)
            {
                case DebitAccount acc:
                    acc.PrintInfo();
                    break;
                case DepositAccount acc:
                    acc.PrintInfo();
                    break;
                case CreditAccount acc:
                    acc.PrintInfo();
                    break;
            }
        }
    }
}