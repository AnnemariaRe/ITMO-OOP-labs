using System;
using System.Collections.Generic;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal balance, Client owner, DateTime expirationDate, List<(int, decimal)> interests)
            : base(balance, owner)
        {
            if (interests == null) throw new NullOrEmptyBanksException("List of interests cannot be null");
            AccountType = "Deposit";
            ExpirationDate = expirationDate;
            SetInterestOnBalance(interests);
        }

        public override bool IsWithdrawAvaliable(decimal value) => Balance >= value && ExpirationDate < DateTime.Now;

        public new void PrintInfo()
        {
            Console.WriteLine("Account type: Credit account");
            Console.WriteLine($"Balance: {Balance}");
            Console.WriteLine($"Expiration date: {ExpirationDate}");
            Console.WriteLine($"Interest on balance: {InterestOnBalance}");
        }

        private void SetInterestOnBalance(List<(int, decimal)> interests)
        {
            for (int i = 0; i < interests.Capacity; i++)
            {
                if (Balance < interests[i].Item1)
                {
                    InterestOnBalance = interests[i].Item2;
                    break;
                }
            }

            if (Balance == 0) InterestOnBalance = 0;
        }
    }
}