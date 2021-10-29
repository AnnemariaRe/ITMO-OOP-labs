using System.IO;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObject
    {
        public JobObject(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new NullOrEmptyBackupException("File path cannot be null");
            FilePath = filePath;
            File = new FileInfo(filePath);
        }

        public FileInfo File { get; }
        public string FilePath { get; }
    }
}