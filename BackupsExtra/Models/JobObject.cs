using System;
using System.IO;
using System.Text;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class JobObject
    {
        public JobObject(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new NullOrEmptyBackupExtraException("File path cannot be null");
            FilePath = filePath;
            File = new FileInfo(filePath);
        }

        public FileInfo File { get; }
        public string FilePath { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File path: {FilePath}, File name: {File.Name}");
            return sb.ToString();
        }
    }
}