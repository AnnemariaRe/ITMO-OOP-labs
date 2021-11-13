using System;
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

        public new void PrintInfo()
        {
            Console.WriteLine("Account type: Debit account");
            Console.WriteLine($"Balance: {Balance}");
            Console.WriteLine($"Interest on Balance: {InterestOnBalance}");
        }
    }
}