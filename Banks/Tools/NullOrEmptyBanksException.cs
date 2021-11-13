namespace Banks.Tools
{
    public class NullOrEmptyBanksException : BanksException
    {
        public NullOrEmptyBanksException(string message)
            : base(message)
        {
        }
    }
}