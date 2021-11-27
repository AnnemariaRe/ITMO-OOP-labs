using System;

namespace BackupsExtra.Tools
{
    public class BackupExtraException : Exception
    {
        public BackupExtraException(string message)
            : base(message)
        {
        }
    }
}