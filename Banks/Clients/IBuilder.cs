namespace Banks.Clients
{
    public interface IBuilder
    {
        IBuilder SetName(string name);
        IBuilder SetSurname(string surname);
        IBuilder SetAdress(string adress);
        IBuilder SetPassportId(int passportId);
        void Reset();
        Client GetInfo();
    }
}