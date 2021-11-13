using System;
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

        public new void PrintInfo()
        {
            Console.WriteLine("Account type: Credit account");
            Console.WriteLine($"Balance: {Balance}");
            Console.WriteLine($"Credit limit: {Limit}");
            Console.WriteLine($"Comission: {Comission}");
        }

        private decimal CalculateComission(decimal value) => value * Comission / 100;
    }
}