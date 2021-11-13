using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Banks
{
    public class CentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; }

        public Bank CreateBank(string name, decimal debitInterest, List<(int, decimal)> depositInterests, decimal creditComission, int creditLimit)
        {
            var newBank = new Bank(name, debitInterest, depositInterests, creditComission, creditLimit);
            Banks.Add(newBank);
            return newBank;
        }

        public Client AddClientToBank(Client client, Guid bankId)
        {
            return Banks.FirstOrDefault(bank => bank.Id == bankId)?.AddNewClient(client.Name, client.Surname, client.Address, client.PassportId);
        }

        public Account CreateAccount(Client client, Guid bankId, string accountType, decimal money)
        {
            var bank = Banks.FirstOrDefault(bank => bank.Id == bankId);
            Account acc = accountType switch
            {
                "Debit" => bank?.CreateDebitAccount(client, money),
                "Deposit" => bank?.CreateDepositAccount(client, money),
                "Credit" => bank?.CreateCreditAccount(client, money),
                _ => null
            };

            return acc;
        }

        public void UpdateInterestOnBalance()
        {
            foreach (var account in Banks.SelectMany(bank => bank.Accounts))
            {
                account.CalculateDayProfit();
                account.UpdateBalance();
            }
        }
    }
}