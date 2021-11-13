using System;
using System.Collections.Generic;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class AccountFactory
    {
        public decimal InterestOnBalance { get; private set; }
        public int Limit { get; private set; }
        public decimal Comission { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public Dictionary<int, decimal> Interests { get; private set; }

        public AccountFactory SetValues(int limit, decimal comission, decimal interestOnBalance, Dictionary<int, decimal> interests)
        {
            if (limit <= 0) throw new NullOrEmptyBanksException("Limit cannot be null or negative");
            Limit = limit;
            if (comission is <= 0 or > 100) throw new NullOrEmptyBanksException("Comission cannot be null, negative or more than 100");
            Comission = comission;
            if (interestOnBalance is <= 0 or > 100) throw new NullOrEmptyBanksException("Interest on balance cannot be null, negative or more than 100");
            InterestOnBalance = interestOnBalance;
            Interests = interests ?? throw new NullOrEmptyBanksException("Interests cannot be null");
            return this;
        }

        public Account CreateAccount(string account, Client owner, decimal balance, DateTime expirationDate = default)
        {
            return account switch
            {
                "Debit" => new DebitAccount(balance, owner, InterestOnBalance),
                "Deposit" => new DepositAccount(balance, owner, ExpirationDate, Interests),
                "Credit" => new CreditAccount(balance, owner, Limit, Comission),
                _ => throw new NullOrEmptyBanksException("Invalid account type")
            };
        }

        public void SetCreditLimit(int limit)
        {
            if (limit <= 0) throw new NullOrEmptyBanksException("Limit cannot be null or negative");
            Limit = limit;
        }

        public void SetCreditComission(decimal comission)
        {
            if (comission <= 0) throw new NullOrEmptyBanksException("Comission cannot be null or negative");
            Comission = comission;
        }

        public void SetDebitInterest(decimal interest)
        {
            if (interest <= 0) throw new NullOrEmptyBanksException("Interest cannot be null or negative");
            InterestOnBalance = interest;
        }

        public void SetDepositInterests(Dictionary<int, decimal> interests)
        {
            Interests = interests ?? throw new NullOrEmptyBanksException("Interests cannot be null");
        }
    }
}