using System;

namespace Backups.Tools
{
    public class BackupException : Exception
    {
        public BackupException(string message)
            : base(message)
        {
        }
    }
}