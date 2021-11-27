using System.Collections.Generic;
using System.Text;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class Repository
    {
        public Repository(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) throw new NullOrEmptyBackupExtraException("Path cannot be null");
            FullPath = fullPath;
            Storages = new List<Storage>();
        }

        public string FullPath { get; }
        public List<Storage> Storages { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Repository path: {FullPath}");
            return sb.ToString();
        }
    }
}