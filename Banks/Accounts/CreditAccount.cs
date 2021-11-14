using System;
using System.Text;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(decimal balance, Client owner, int limit, decimal comission)
            : base(balance, owner)
        {
            if (limit <= 0) throw new NullOrEmptyBanksException("Limit cannot be null or negative");
            if (comission == 0) throw new NullOrEmptyBanksException("Comission cannot be null or negative");
            AccountType = "Credit";
            Limit = limit;
            Comission = comission;
        }

        public override bool IsWithdrawAvaliable(decimal value) => Math.Abs(Balance - value) < Limit;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Account type: Credit account");
            sb.AppendLine($"Balance: {Balance}");
            sb.AppendLine($"Credit limit: {Limit}");
            sb.AppendLine($"Comission: {Comission}");
            return sb.ToString();
        }

        private decimal CalculateComission(decimal value) => value * Comission / 100;
    }
}