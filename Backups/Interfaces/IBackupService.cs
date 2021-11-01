using System;
using Backups.Models;

namespace Backups.Interfaces
{
    public interface IBackupService
    {
        public BackupJob CreateBackupJob(string storageType);

        public RestorePoint CreateRestorePoint(Guid jobId);

        public JobObject AddJobObject(Guid jobId, JobObject jobObject);

        public void RemoveJobObject(Guid jobId, JobObject jobObject);

        public void SaveBackup();
    }
}