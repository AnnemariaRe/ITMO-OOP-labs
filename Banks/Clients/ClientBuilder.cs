using System.Runtime.CompilerServices;
using Banks.Tools;

namespace Banks.Clients
{
    public class ClientBuilder : IClientBuilder
    {
        private Client _client;

        public IClientBuilder SetName(string name)
        {
            _client.Name = name;
            return this;
        }

        public IClientBuilder SetSurname(string surname)
        {
            _client.Surname = surname;
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

        public void Reset()
        {
            _client = new Client();
        }

        public Client Build()
        {
            if (_client.Name == null || _client.Surname == null)
                throw new NullOrEmptyBanksException("Cannot create client without name or surname");
            var currentClient = _client;
            Reset();
            return currentClient;
        }
    }
}