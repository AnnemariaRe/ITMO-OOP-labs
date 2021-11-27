using System;
using System.Collections.Generic;
using System.Text;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class RestorePoint
    {
        private List<Storage> _storages;
        public RestorePoint(List<Storage> storages, int number, string pointName, DateTime creationTime, string storageType)
        {
            if (number is 0) throw new NullOrEmptyBackupExtraException("Number cannot be null");
            Number = number;
            PointName = pointName + $"_{number}";
            CreationTime = creationTime;
            _storages = storages ?? throw new NullOrEmptyBackupExtraException("Storages cannot be null");
            StorageType = storageType switch
            {
                StorageTypes.SingleStorage => storageType,
                StorageTypes.SplitStorages => storageType,
                _ => throw new BackupExtraException("Invalid storage type name")
            };
        }

        public string PointName { get; }
        public DateTime CreationTime { get; }
        public List<Storage> Storages => _storages;
        public int Number { get; }
        public string StorageType { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Restore point name: {PointName}, Creation time: {CreationTime}");
            return sb.ToString();
        }
    }
}