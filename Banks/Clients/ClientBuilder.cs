using System.Runtime.CompilerServices;

namespace Banks.Clients
{
    public class ClientBuilder : IBuilder
    {
        private Client _client = new Client();

        public IBuilder SetName(string name)
        {
            _client.Name = name;
            return this;
        }

        public IBuilder SetSurname(string surname)
        {
            _client.Surname = surname;
            return this;
        }

        public IBuilder SetAdress(string adress = null)
        {
            _client.Address = adress;
            return this;
        }

        public IBuilder SetPassportId(int passportId = 0)
        {
            _client.PassportId = passportId;
            return this;
        }

        public void Reset()
        {
            _client = new Client();
        }

        public Client GetInfo()
        {
            var currentClient = _client;
            currentClient.SetVerification();
            Reset();
            return currentClient;
        }
    }
}