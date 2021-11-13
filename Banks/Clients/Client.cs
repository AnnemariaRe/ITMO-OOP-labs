using System;
using System.Collections.Generic;
using System.Text;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Clients
{
    public class Client
    {
        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; protected internal set; }
        public string Surname { get; protected internal set; }
        public string Address { get; protected internal set; }
        public int PassportId { get; protected internal set; }
        public bool IsVerified => PassportId > 0 && !string.IsNullOrWhiteSpace(Address);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Surname: {Surname}");
            sb.AppendLine($"Adress: {Address}");
            sb.AppendLine($"Passport id: {PassportId}");
            return sb.ToString();
        }
    }
}