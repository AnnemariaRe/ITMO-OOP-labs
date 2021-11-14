using System;
using System.Text;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(decimal balance, Client owner, decimal interestOnBalance)
            : base(balance, owner)
        {
            if (interestOnBalance == 0) throw new NullOrEmptyBanksException("Interest on balance cannot be null or negative");
            AccountType = "Debit";
            InterestOnBalance = interestOnBalance;
        }

        public override bool IsWithdrawAvaliable(decimal value) => Balance >= value;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Account type: Debit account");
            sb.AppendLine($"Balance: {Balance}");
            sb.AppendLine($"Interest on Balance: {InterestOnBalance}");
            return sb.ToString();
        }
    }
}