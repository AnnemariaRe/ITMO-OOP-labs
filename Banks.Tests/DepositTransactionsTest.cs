using System.Collections.Generic;
using Banks.Banks;
using Banks.Clients;
using NUnit.Framework;

namespace Banks.Tests
{
    public class DepositTransactionsTest
    {
        private CentralBank _service;
        private Bank _bank;
        private List<(int, decimal)> _interests;
        
        [SetUp]
        public void Setup()
        {
            _service = new CentralBank();
            _interests = new List<(int, decimal)>
            {
                (5000, 3),
                (50000, 4),
                (10000, 5)
            };
            _bank = _service.CreateBank("AlfaBank", 3, _interests, 20, 30000);
        }

        [Test]
        public void ReplenishTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Deposit", 5000);
            
            // Act
            _bank.ReplenishMoney(account.Id, 10000);

            // Assert
            Assert.AreEqual(15000, account.Balance);
        }

        [Test]
        public void WithdrawTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Deposit", 5000);
            
            // Act
            _bank.WithdrawMoney(account.Id, 1000);

            // Assert
            Assert.AreEqual(4000, account.Balance);
        }
        
        [Test]
        public void TransferTest()
        {
            // Assert
            var client1 = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account1 = _service.CreateAccount(client1, _bank.Id, "Deposit", 20000);
            var client2 = _service.AddClientToBank(new ClientBuilder().SetName("Ksenia").SetSurname("Vasutinkskaya").GetInfo(), _bank.Id);
            var account2 = _service.CreateAccount(client2, _bank.Id, "Deposit", 10);
            
            // Act
            _bank.TransferMoney(account1.Id, account2.Id, 10000);

            // Assert
            Assert.AreEqual(10000, account1.Balance);
            Assert.AreEqual(10010, account2.Balance);
        }

        [Test]
        public void UndoTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Debit", 5000);
            
            // Act
            _bank.WithdrawMoney(account.Id, 10);
            _bank.UndoTransaction(account.Id);

            // Assert
            Assert.AreEqual(5000, account.Balance);
        }
    }
}