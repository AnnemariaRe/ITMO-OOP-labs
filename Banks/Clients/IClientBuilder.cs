namespace Banks.Clients
{
    public interface IClientBuilder
    {
        IClientBuilder SetNameAndSurname(string name, string surname);
        IClientBuilder SetAddress(string address);
        IClientBuilder SetPassportId(int passportId);
        Client Build();
    }
}