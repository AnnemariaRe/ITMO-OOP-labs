using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal balance, Client owner, DateTime expirationDate, Dictionary<int, decimal> interests)
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

        private void SetInterestOnBalance(Dictionary<int, decimal> interests)
        {
            foreach (var interest in interests.Where(interest => Balance < interest.Key))
            {
                InterestOnBalance = interest.Value;
                break;
            }

            if (Balance == 0) InterestOnBalance = 0;
        }
    }
}