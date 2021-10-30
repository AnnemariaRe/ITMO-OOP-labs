using System;
using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Models
{
    public class RestorePoint
    {
        private List<Storage> _storages;
        public RestorePoint(List<Storage> storages, int number, string pointName, DateTime creationTime)
        {
            if (number is 0) throw new NullOrEmptyBackupException("Number cannot be null");
            Number = number;
            PointName = pointName + $"_{number}";
            CreationTime = creationTime;
            _storages = storages ?? throw new NullOrEmptyBackupException("Storages cannot be null");
        }

        public string PointName { get; }
        public DateTime CreationTime { get; }
        public List<Storage> Storages => _storages;
        public int Number { get; }
    }
}