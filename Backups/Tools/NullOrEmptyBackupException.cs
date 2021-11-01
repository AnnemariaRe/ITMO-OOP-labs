namespace Backups.Tools
{
    public class NullOrEmptyBackupException : BackupException
    {
        public NullOrEmptyBackupException(string message)
            : base(message)
        {
        }
    }
}