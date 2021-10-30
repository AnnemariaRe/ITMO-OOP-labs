using System.IO;
using Backups.Models;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackupInSingleStorageTest
    {
        private BackupService service; 
        
        [SetUp]
        public void Setup()
        {
            service = new BackupService();
            File.Create(service.Repository.FullPath + @"\" + "bebra.txt").Close();
            File.Create(service.Repository.FullPath + @"\" + "aboba.txt").Close();
            File.Create(service.Repository.FullPath + @"\" + "kek.txt").Close();
        }

        [Test]
        public void BackupInSingleStorage()
        {
            // Arrange
            var backupJob = service.CreateBackupJob("SingleStorage");
            var jobObject1 = service.AddJobObject(backupJob.Id, new JobObject(service.Repository.FullPath + @"\" + "bebra.txt"));
            var jobObject2 = service.AddJobObject(backupJob.Id, new JobObject(service.Repository.FullPath + @"\" + "aboba.txt"));
            var jobObject3 = service.AddJobObject(backupJob.Id, new JobObject(service.Repository.FullPath + @"\" + "kek.txt"));

            // Act
            service.CreateRestorePoint(backupJob.Id);

            // Assert
            Assert.AreEqual(1, service.Repository.Storages.Count);
        }
    }
}