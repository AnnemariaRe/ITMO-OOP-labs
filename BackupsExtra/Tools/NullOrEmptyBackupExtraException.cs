using BackupsExtra.Tools;

namespace BackupsExtra.Tools
{
    public class NullOrEmptyBackupExtraException : BackupExtraException
    {
        public NullOrEmptyBackupExtraException(string message)
            : base(message)
        {
        }
    }
}