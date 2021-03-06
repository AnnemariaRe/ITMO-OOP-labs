using System.Collections.Generic;
using System.Linq;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class DebitTransactionsTest
    {
        private CentralBank _service;
        private Bank _bank;
        private Dictionary<int, decimal> _interests;
        
        [SetUp]
        public void Setup()
        {
            _service = new CentralBank();
            _interests = new Dictionary<int, decimal>();
            _interests.Add(5000, 3);
            _interests.Add(50000, 4);
            _interests.Add(10000, 5);
            
            _bank = _service.CreateBank("AlfaBank", 3, _interests, 20, 30000);
        }

        [Test]
        public void ReplenishTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Debit", 12937);
            
            // Act
            _bank.ReplenishMoney(account.Id, 10000);

            // Assert
            Assert.AreEqual(22937, account.Balance);
        }

        [Test]
        public void WithdrawExceptionTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Debit", 5);

            // Assert
            Assert.Catch<BanksException>(() =>
            {
                _bank.WithdrawMoney(account.Id, 30);
            });
        }
        
        [Test]
        public void TransferTest()
        {
            // Assert
            var client1 = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account1 = _service.CreateAccount(client1, _bank.Id, "Debit", 12937);
            var client2 = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Ksenia", "Vasutinkskaya").Build(), _bank.Id);
            var account2 = _service.CreateAccount(client2, _bank.Id, "Debit", 10);
            
            // Act
            _bank.TransferMoney(account1.Id, account2.Id, 10000);

            // Assert
            Assert.AreEqual(2937, account1.Balance);
            Assert.AreEqual(10010, account2.Balance);
        }

        [Test]
        public void UndoTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Debit", 12937);
            
            // Act
            _bank.WithdrawMoney(account.Id, 10000);
            _bank.UndoTransaction(account.Id);

            // Assert
            Assert.AreEqual(12937, account.Balance);
        }
    }
}