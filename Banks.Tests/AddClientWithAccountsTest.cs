using System.Collections.Generic;
using System.Linq;
using Banks.Banks;
using Banks.Clients;
using NUnit.Framework;

namespace Banks.Tests
{
    public class AddClientWithAccountsTest
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
        public void AddClientWithDebitAccountTest()
        {
            // Act
            var client = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Debit", 12937);

            // Assert
            Assert.AreEqual("Debit", account.AccountType);
            Assert.AreEqual(3, account.InterestOnBalance);
            Assert.AreEqual(12937, _bank.Clients[client].FirstOrDefault(acc => acc.Owner == client).Balance);
        }
        
        [Test]
        public void AddClientWithDepositAccountTest()
        {
            // Act
            var client = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Deposit", 7345);

            // Assert
            Assert.AreEqual(4, account.InterestOnBalance);
            Assert.AreEqual(7345, _bank.Clients[client].FirstOrDefault(acc => acc.Owner == client).Balance);
        }
        
        [Test]
        public void AddClientWithCreditAccountTest()
        {
            // Act
            var client = _service.AddClientToBank(new ClientBuilder().SetName("Annemarija").SetSurname("Repenko").GetInfo(), _bank.Id);
            var account = _service.CreateAccount(client, _bank.Id, "Credit", 1);

            // Assert
            Assert.AreEqual(20, account.Comission);
            Assert.AreEqual(30000, account.Limit);
            Assert.AreEqual(1, _bank.Clients[client].FirstOrDefault(acc => acc.Owner == client).Balance);
        }
    }
}