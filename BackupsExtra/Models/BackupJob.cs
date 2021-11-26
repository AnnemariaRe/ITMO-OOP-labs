using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupsExtra.Cleaner;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    [Serializable]
    public class BackupJob
    {
        private readonly List<JobObject> _filesToBackup;
        private readonly CleanerFactory _cleanerFactory;

        public BackupJob(string storageType, string jobName, string cleanType, DateTime dateToClean, int limitToClean, string hybridType)
        {
            if (string.IsNullOrEmpty(storageType)) throw new NullOrEmptyBackupExtraException("Storage type cannot be null");
            if (string.IsNullOrEmpty(jobName)) throw new NullOrEmptyBackupExtraException("Job name cannot be null");
            if (string.IsNullOrEmpty(cleanType)) throw new NullOrEmptyBackupExtraException("Job name cannot be null");
            if (storageType != StorageTypes.SingleStorage && storageType != StorageTypes.SplitStorages)
                throw new BackupExtraException("Incorrect storage type");

            Id = Guid.NewGuid();
            StorageType = storageType;
            JobName = jobName;
            _cleanerFactory = new CleanerFactory();
            _cleanerFactory.SetValues(
                limitToClean,
                dateToClean,
                new List<ICleaner>() { new DateCleaner(dateToClean), new LimitCleaner(limitToClean) },
                hybridType);
            Cleaner = _cleanerFactory.CreateCleaner(cleanType);

            RestorePoints = new List<RestorePoint>();
            _filesToBackup = new List<JobObject>();
        }

        public Guid Id { get; }
        public List<RestorePoint> RestorePoints { get; set; }
        public string JobName { get; }
        public List<JobObject> FilesToBackup => _filesToBackup;
        public string StorageType { get; }
        public ICleaner Cleaner { get; }

        public void AddJobObject(string filePath, long size)
        {
            _filesToBackup.Add(new JobObject(filePath));
        }

        public void RemoveJobObject(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new NullOrEmptyBackupExtraException("File path cannot be null");
            _filesToBackup.Remove(FilesToBackup.FirstOrDefault(f => f.FilePath == filePath));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Job name: {JobName}, Storage type: {StorageType}");
            return sb.ToString();
        }
    }
}