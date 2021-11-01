using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Models
{
    public class Repository
    {
        public Repository(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) throw new NullOrEmptyBackupException("Path cannot be null");
            FullPath = fullPath;
            Storages = new List<Storage>();
        }

        public string FullPath { get; }
        public List<Storage> Storages { get; }
    }
}