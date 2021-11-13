namespace Banks.Clients
{
    public interface IClientBuilder
    {
        IClientBuilder SetName(string name);
        IClientBuilder SetSurname(string surname);
        IClientBuilder SetAddress(string adress);
        IClientBuilder SetPassportId(int passportId);
        void Reset();
        Client Build();
    }
}