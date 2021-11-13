using System.Collections.Generic;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class CreditTransactionsTest
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
            
            _bank = _service.CreateBank("AlfaBank", 3, _interests, 20, 3000);
        }

        [Test]
        public void ReplenishTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Credit", 5000);
            
            // Act
            _bank.ReplenishMoney(account.Id, 10000);

            // Assert
            Assert.AreEqual(15000, account.Balance);
        }

        [Test]
        public void WithdrawExceptionTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Credit", 5);

            // Assert
            Assert.Catch<BanksException>(() =>
            {
                _bank.WithdrawMoney(account.Id, 3010);
            });
        }
        
        [Test]
        public void TransferTest()
        {
            // Assert
            var client1 = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account1 = _service.CreateAccount(client1, _bank.Id, "Credit", 200);
            var client2 = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Ksenia", "Vasutinkskaya").Build(), _bank.Id);
            var account2 = _service.CreateAccount(client2, _bank.Id, "Credit", 10);
            
            // Act
            _bank.TransferMoney(account1.Id, account2.Id, 100);

            // Assert
            Assert.AreEqual(100, account1.Balance);
            Assert.AreEqual(110, account2.Balance);
        }

        [Test]
        public void UndoTest()
        {
            // Assert
            var client = _service.AddClientToBank(new ClientBuilder().SetNameAndSurname("Annemarija", "Repenko").Build(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Debit", 5000);
            
            // Act
            _bank.WithdrawMoney(account.Id, 10);
            _bank.UndoTransaction(account.Id);

            // Assert
            Assert.AreEqual(5000, account.Balance);
        }
    }
}