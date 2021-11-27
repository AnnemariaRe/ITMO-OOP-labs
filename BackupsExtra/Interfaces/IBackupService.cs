using System;
using System.Collections.Generic;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface IBackupService
    {
        public BackupJob CreateBackupJob(string storageType, string cleanType, DateTime date, int limit, string hybridType);

        public RestorePoint CreateRestorePoint(Guid jobId);

        public JobObject AddJobObject(Guid jobId, JobObject jobObject);

        public void RemoveJobObject(Guid jobId, JobObject jobObject);

        public void SaveBackup();

        public void DataRecovery(string pointName, string path = null);

        public void SaveData(string fileName);

        public List<BackupJob> LoadData(string fileName);
    }
}