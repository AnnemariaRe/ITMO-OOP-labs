using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Clients;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.Banks
{
    public class CentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; internal set; }

        public Bank CreateBank(string name, decimal debitInterest, List<(int, decimal)> depositInterests, decimal creditComission, int creditLimit)
        {
            var newBank = new Bank(name, debitInterest, depositInterests, creditComission, creditLimit);
            Banks.Add(newBank);
            return newBank;
        }

        public Client AddClientToBank(Client client, Guid bankId)
        {
            return Banks.FirstOrDefault(bank => bank.Id == bankId)?.AddNewClient(client);
        }

        public Account CreateAccount(Client client, Guid bankId, string accountType, decimal money)
        {
            var bank = Banks.FirstOrDefault(bank => bank.Id == bankId);
            return accountType switch
            {
                "Debit" => bank?.CreateDebitAccount(client, money),
                "Deposit" => bank?.CreateDepositAccount(client, money),
                "Credit" => bank?.CreateCreditAccount(client, money),
                _ => null
            };
        }

        public void TransferMoneyBetweenBanks(Account accountFrom, Account accountTo, decimal money)
        {
            if (accountFrom is null || accountTo is null) throw new NullOrEmptyBanksException("Accounts cannot be null");
            if (!Banks.Any(bank => bank.Accounts.Contains(accountFrom))) throw new NullOrEmptyBanksException("Account is not find");
            if (!Banks.Any(bank => bank.Accounts.Contains(accountTo))) throw new NullOrEmptyBanksException("Account is not find");
            var transaction = new Transaction(accountFrom);
            transaction.Transfer(accountTo, money);
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