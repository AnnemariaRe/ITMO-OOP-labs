using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Interfaces;
using Backups.Models;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupService : IBackupService
    {
        private const string SingleStorage = "SingleStorage";
        private const string SplitStorages = "SplitStorages";

        private readonly IAlgorithm _singleStorageAlgorithm = new SingleStorageAlgorithm();
        private readonly IAlgorithm _splitStoragesAlgorithm = new SplitStoragesAlgorithm();
        private int _jobCounter;
        private int _pointCounter;

        public BackupService()
        {
            Repository = new Repository(@"C:\Users\PC\source\repos\AnnemariaRe\Backups\Repository");
            BackupJobs = new List<BackupJob>();
            _pointCounter = 1;
            _jobCounter = 1;
        }

        public List<BackupJob> BackupJobs { get; }
        public Repository Repository { get; }
        public BackupJob CreateBackupJob(string storageType)
        {
            if (string.IsNullOrEmpty(storageType)) throw new NullOrEmptyBackupException("Storage type cannot be null");
            var newBackupJob = new BackupJob(storageType, $"Joba_{_jobCounter}");
            _jobCounter++;
            if (BackupJobs.Contains(newBackupJob)) throw new BackupException("Current backup job is already existed");
            BackupJobs.Add(newBackupJob);
            return newBackupJob;
        }

        public RestorePoint CreateRestorePoint(Guid jobId)
        {
            var backupJob = BackupJobs.FirstOrDefault(job => job.Id == jobId);
            if (backupJob is null) throw new NullOrEmptyBackupException("Backup job object cannot be null");

            var storages = new List<Storage>();
            switch (backupJob.StorageType)
            {
                case SingleStorage:
                {
                    storages.AddRange(_singleStorageAlgorithm.SaveCopies(_pointCounter, backupJob.FilesToBackup));
                    break;
                }

                case SplitStorages:
                {
                    storages.AddRange(_splitStoragesAlgorithm.SaveCopies(_pointCounter, backupJob.FilesToBackup));
                    break;
                }

                default: throw new BackupException("Invalid storage type name");
            }

            var newRestorePoint = new RestorePoint(storages, _pointCounter, "RestorePoint");
            _pointCounter++;
            Repository.Storages.AddRange(storages);
            backupJob.RestorePoints.Add(newRestorePoint);
            return newRestorePoint;
        }

        public JobObject AddJobObject(Guid jobId, JobObject jobObject)
        {
            if (jobObject is null) throw new NullOrEmptyBackupException("Job object cannot be null");
            BackupJobs.FirstOrDefault(job => job.Id == jobId)?.FilesToBackup.Add(jobObject);
            return jobObject;
        }

        public void RemoveJobObject(Guid jobId, JobObject jobObject)
        {
            if (jobObject is null) throw new NullOrEmptyBackupException("Job object cannot be null");
            BackupJobs.FirstOrDefault(job => job.Id == jobId)?.FilesToBackup.Remove(jobObject);
        }

        public void SaveBackup()
        {
            var directory = new DirectoryInfo(Repository.FullPath);
            if (directory.Exists) Console.WriteLine("Directory is already exist");
            else Directory.CreateDirectory(directory.FullName);

            foreach (var joba in BackupJobs)
            {
                Directory.CreateDirectory(Repository.FullPath + @"\" + joba.JobName);
                foreach (var point in joba.RestorePoints)
                {
                    Directory.CreateDirectory(Repository.FullPath + @"\" + joba.JobName + @"\" + point.PointName);
                    foreach (var storage in point.Storages)
                    {
                        storage.Zip.Save(storage.StorageName);
                        File.Create(Repository.FullPath + @"\" + joba.JobName + @"\" + point.PointName + @"\" + storage.Zip.Name);
                    }
                }
            }
        }
    }
}