using System;
using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Models
{
    public class RestorePoint
    {
        public RestorePoint(List<Storage> storages, int number, string pointName)
        {
            if (number is 0) throw new NullOrEmptyBackupException("Number cannot be null");
            Number = number;
            PointName = pointName + $"_{number}";
            CreationTime = DateTime.Now;
            Storages = storages ?? throw new NullOrEmptyBackupException("Storages cannot be null");
        }

        public string PointName { get; }
        public DateTime CreationTime { get; }
        public List<Storage> Storages { get; }
        public int Number { get; }
    }
}