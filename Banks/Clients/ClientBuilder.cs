using System.Runtime.CompilerServices;
using Banks.Tools;

namespace Banks.Clients
{
    public class ClientBuilder : IClientBuilder
    {
        private Client _client;

        public IClientBuilder SetNameAndSurname(string name, string surname)
        {
            _client = new Client(name, surname);
            return this;
        }

        public IClientBuilder SetAddress(string address = null)
        {
            _client.Address = address;
            return this;
        }

        public IClientBuilder SetPassportId(int passportId = 0)
        {
            _client.PassportId = passportId;
            return this;
        }

        public Client Build()
        {
            if (_client.Name == null || _client.Surname == null)
                throw new NullOrEmptyBanksException("Cannot create client without name or surname");
            var currentClient = _client;
            _client.Address = null;
            _client.PassportId = 0;
            return currentClient;
        }
    }
}