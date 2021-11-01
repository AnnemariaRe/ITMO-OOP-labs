using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        private List<JobObject> _filesToBackup;

        public BackupJob(string storageType, string jobName)
        {
            if (string.IsNullOrEmpty(storageType)) throw new NullOrEmptyBackupException("Storage type cannot be null");
            if (string.IsNullOrEmpty(jobName)) throw new NullOrEmptyBackupException("Job name cannot be null");
            Id = Guid.NewGuid();
            if (storageType != EnumStorageType.SingleStorage && storageType != EnumStorageType.SplitStorages)
                throw new BackupException("Incorrect storage type");
            StorageType = storageType;
            JobName = jobName;
            RestorePoints = new List<RestorePoint>();
            _filesToBackup = new List<JobObject>();
        }

        public Guid Id { get; }
        public List<RestorePoint> RestorePoints { get; }
        public string JobName { get; }
        public List<JobObject> FilesToBackup => _filesToBackup;
        public string StorageType { get; }

        public void AddJobObject(string filePath, long size)
        {
            _filesToBackup.Add(new JobObject(filePath));
        }

        public void RemoveJobObject(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new NullOrEmptyBackupException("File path cannot be null");
            _filesToBackup.Remove(FilesToBackup.FirstOrDefault(f => f.FilePath == filePath));
        }
    }
}