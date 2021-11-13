using System;
using System.Collections.Generic;
using System.Text;
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Account type: Deposit account");
            sb.AppendLine($"Balance: {Balance}");
            sb.AppendLine($"Expiration date: {ExpirationDate}");
            sb.AppendLine($"Interest on balance: {InterestOnBalance}");
            return sb.ToString();
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