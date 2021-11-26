using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BackupsExtra.Cleaner;
using BackupsExtra.Interfaces;
using BackupsExtra.Logger;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class BackupService : IBackupService
    {
        private readonly IAlgorithm _singleStorageAlgorithm = new SingleStorageAlgorithm();
        private readonly IAlgorithm _splitStoragesAlgorithm = new SplitStoragesAlgorithm();
        private readonly BinaryFormatter _serializer;
        private readonly ILogger _logger;
        private int _jobCounter;
        private int _pointCounter;

        public BackupService()
        {
            _logger = new ConsoleLogger();
            _serializer = new BinaryFormatter();
            Repository = new Repository(Directory.GetCurrentDirectory());
            BackupJobs = new List<BackupJob>();
            _pointCounter = 1;
            _jobCounter = 1;
        }

        public List<BackupJob> BackupJobs { get; }
        public Repository Repository { get; }

        public BackupJob CreateBackupJob(string storageType, string cleanType, DateTime date = default, int limit = 1, string hybridType = "All")
        {
            _logger.Info("Request to create backup job....");
            var newBackupJob = new BackupJob(storageType, $"Joba_{_jobCounter}", cleanType, date, limit, hybridType);
            _jobCounter++;
            if (BackupJobs.Contains(newBackupJob)) throw new BackupExtraException("Current backup job is already existed");
            BackupJobs.Add(newBackupJob);
            _logger.Debug($"{newBackupJob.ToString()} is created");
            return newBackupJob;
        }

        public RestorePoint CreateRestorePoint(Guid jobId)
        {
            _logger.Info("Request to create restore point....");
            var backupJob = BackupJobs.FirstOrDefault(job => job.Id == jobId);
            if (backupJob is null) throw new NullOrEmptyBackupExtraException("Backup job object cannot be null");

            var storages = new List<Storage>();
            switch (backupJob.StorageType)
            {
                case StorageTypes.SingleStorage:
                {
                    _logger.Info("Saving copies to single storage....");
                    storages.AddRange(_singleStorageAlgorithm.SaveCopies(backupJob.FilesToBackup));
                    break;
                }

                case StorageTypes.SplitStorages:
                {
                    _logger.Info("Saving copies to split storages....");
                    storages.AddRange(_splitStoragesAlgorithm.SaveCopies(backupJob.FilesToBackup));
                    break;
                }

                default: throw new BackupExtraException("Invalid storage type name");
            }

            var newRestorePoint = new RestorePoint(storages, _pointCounter, "RestorePoint", DateTime.Now, backupJob.StorageType);
            _pointCounter++;
            Repository.Storages.AddRange(storages);
            backupJob.RestorePoints.Add(newRestorePoint);
            _logger.Debug($"{newRestorePoint.ToString()} is created");

            BackupJobs.FirstOrDefault(job => job.Id == jobId).RestorePoints = backupJob.Cleaner.Clean(backupJob.RestorePoints);
            return newRestorePoint;
        }

        public JobObject AddJobObject(Guid jobId, JobObject jobObject)
        {
            _logger.Info("Request to add new job object");
            if (jobObject is null) throw new NullOrEmptyBackupExtraException("Job object cannot be null");
            BackupJobs.FirstOrDefault(job => job.Id == jobId)?.FilesToBackup.Add(jobObject);
            _logger.Debug($"{jobObject.ToString()} is added to {BackupJobs.FirstOrDefault(job => job.Id == jobId)?.ToString()}");
            return jobObject;
        }

        public void RemoveJobObject(Guid jobId, JobObject jobObject)
        {
            _logger.Warning("Attempt to remove job object!");
            if (jobObject is null) throw new NullOrEmptyBackupExtraException("Job object cannot be null");
            BackupJobs.FirstOrDefault(job => job.Id == jobId)?.FilesToBackup.Remove(jobObject);
            _logger.Debug($"{jobObject.ToString()} is removed");
        }

        public void SaveBackup()
        {
            _logger.Info("Saving backup...");
            var directory = new DirectoryInfo(Repository.FullPath);
            if (directory.Exists) throw new NullOrEmptyBackupExtraException("Directory is already exist");
            Directory.CreateDirectory(directory.FullName);

            foreach (var joba in BackupJobs)
            {
                _logger.Info($"Creating job directory - {joba.JobName}....");
                Directory.CreateDirectory(Repository.FullPath + Path.DirectorySeparatorChar + joba.JobName);
                foreach (var point in joba.RestorePoints)
                {
                    _logger.Info($"Creating restore point directory - {point.PointName}....");
                    Directory.CreateDirectory(Repository.FullPath + Path.DirectorySeparatorChar + joba.JobName + Path.DirectorySeparatorChar + point.PointName);
                    foreach (var storage in point.Storages)
                    {
                        _logger.Info($"Creating zip file - {storage.Zip.Name}....");
                        storage.Zip.Save(storage.StorageName);
                        File.Create(Repository.FullPath + Path.DirectorySeparatorChar + joba.JobName + Path.DirectorySeparatorChar + point.PointName + Path.DirectorySeparatorChar + storage.Zip.Name).Close();
                    }
                }
            }

            _logger.Debug("Backup is saved");
        }

        public void DataRecovery(string pointName, string path = null)
        {
            if (string.IsNullOrWhiteSpace(pointName))
                throw new NullOrEmptyBackupExtraException("Point name cannot be null");
            _logger.Info($"{pointName} is recovering....");

            RestorePoint point = BackupJobs
                .FirstOrDefault(job => job.RestorePoints.Any(point => point.PointName == pointName))?.RestorePoints
                .FirstOrDefault(point => point.PointName == pointName);
            if (point == null) throw new NullOrEmptyBackupExtraException("Cannot find current restore point");

            string fullPath = null;
            if (string.IsNullOrWhiteSpace(path)) fullPath = Repository.FullPath + Path.DirectorySeparatorChar;
            if (!string.IsNullOrWhiteSpace(path))
            {
                Directory.CreateDirectory(path);
                fullPath = path + Path.DirectorySeparatorChar;
            }

            foreach (var storage in point.Storages)
            {
                if (File.Exists(fullPath + storage.StorageName))
                {
                    File.SetAttributes(fullPath + storage.StorageName, FileAttributes.Normal);
                    File.Delete(fullPath + storage.StorageName);
                }

                File.Create(fullPath + storage.StorageName).Close();
            }

            if (path == null) _logger.Debug($"Data from restore point {pointName} is recovered to original location");
            if (path != null) _logger.Debug($"Data from restore point {pointName} is recovered to different location");
        }

        public void SaveData(string fileName)
        {
            _logger.Info("Saving current backups data...");
            if (string.IsNullOrWhiteSpace(fileName))
                throw new NullOrEmptyBackupExtraException("File name cannot be null");
            _serializer.Serialize(new FileStream(fileName + ".dat", FileMode.OpenOrCreate), this.BackupJobs);
            _logger.Debug($"Data is saved to {fileName}.bin");
        }

        public List<BackupJob> LoadData(string fileName)
        {
            _logger.Info("Loading previous backups data...");
            if (!File.Exists(fileName + ".dat")) throw new NullOrEmptyBackupExtraException("Invalid file name");
            return (List<BackupJob>)_serializer.Deserialize(new FileStream(fileName + ".dat", FileMode.OpenOrCreate));
        }
    }
}