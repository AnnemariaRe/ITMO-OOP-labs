using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        private const string SingleStorage = "SingleStorage";
        private const string SplitStorages = "SplitStorages";

        public BackupJob(string storageType, string jobName)
        {
            if (string.IsNullOrEmpty(storageType)) throw new NullOrEmptyBackupException("Storage type cannot be null");
            if (string.IsNullOrEmpty(jobName)) throw new NullOrEmptyBackupException("Job name cannot be null");
            Id = Guid.NewGuid();
            if (storageType != SingleStorage && storageType != SplitStorages)
                throw new BackupException("Incorrect storage type");
            StorageType = storageType;
            JobName = jobName;
            RestorePoints = new List<RestorePoint>();
            FilesToBackup = new List<JobObject>();
        }

        public Guid Id { get; }
        public List<RestorePoint> RestorePoints { get; }
        public string JobName { get; }
        public List<JobObject> FilesToBackup { get; }
        public string StorageType { get; }

        public void AddJobObject(string filePath, long size)
        {
            FilesToBackup.Add(new JobObject(filePath));
        }

        public void RemoveJobObject(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new NullOrEmptyBackupException("l");
            FilesToBackup.Remove(FilesToBackup.FirstOrDefault(f => f.FilePath == filePath));
        }
    }
}