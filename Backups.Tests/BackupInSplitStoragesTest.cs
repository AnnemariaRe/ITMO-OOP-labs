using System.IO;
using System.Linq;
using Backups.Models;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackupInSplitStoragesTest
    {
        private BackupService service; 
        
        [SetUp]
        public void Setup()
        {
            service = new BackupService();
            File.Create(service.Repository.FullPath + Path.PathSeparator + "bebra.txt").Close();
            File.Create(service.Repository.FullPath + Path.PathSeparator + "aboba.txt").Close();
        }

        [Test]
        public void BackupInSplitStorages()
        {
            // Arrange
            var backupJob = service.CreateBackupJob("SplitStorages");
            var jobObject1 = service.AddJobObject(backupJob.Id, new JobObject(service.Repository.FullPath + Path.PathSeparator + "bebra.txt"));
            var jobObject2 = service.AddJobObject(backupJob.Id, new JobObject(service.Repository.FullPath + Path.PathSeparator + "aboba.txt"));

            // Act
            service.CreateRestorePoint(backupJob.Id);
            service.RemoveJobObject(backupJob.Id, jobObject1);
            service.CreateRestorePoint(backupJob.Id);
            BackupJob first = service.BackupJobs.FirstOrDefault(backup => backup.Id == backupJob.Id);
            
            // Assert
            Assert.AreEqual(2, first.RestorePoints.Count);
            Assert.AreEqual(3, service.Repository.Storages.Count);
        }
    }
}