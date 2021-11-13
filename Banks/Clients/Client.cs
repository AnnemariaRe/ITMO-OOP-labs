using System;
using System.Collections.Generic;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Clients
{
    public class Client
    {
        public Client()
        {
        }

        public string Name { get; protected internal set; }
        public string Surname { get; protected internal set; }
        public string Address { get; protected internal set; }
        public int PassportId { get; protected internal set; }
        public bool IsVerified { get; private set; }

        public bool SetVerification()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname))
                throw new NullOrEmptyBanksException("Client name cannot be null");
            if (string.IsNullOrEmpty(Address) || PassportId == 0) return IsVerified = false;
            return IsVerified = true;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Surname: {Surname}");
            Console.WriteLine($"Adress: {Address}");
            Console.WriteLine($"Passport id: {PassportId}");
        }
    }
}