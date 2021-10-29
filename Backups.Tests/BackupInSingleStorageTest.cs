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
        }

        [Test]
        public void BackupInSingleStorage()
        {
            // Arrange
            var backupJob = service.CreateBackupJob("SingleStorage");
            var jobObject1 = service.AddJobObject(backupJob.Id, new JobObject(@"C:\Users\PC\source\repos\AnnemariaRe\Backups\FilesToBackup\aboba.txt"));
            var jobObject2 = service.AddJobObject(backupJob.Id, new JobObject(@"C:\Users\PC\source\repos\AnnemariaRe\Backups\FilesToBackup\bebra.txt"));
            var jobObject3 = service.AddJobObject(backupJob.Id, new JobObject(@"C:\Users\PC\source\repos\AnnemariaRe\Backups\FilesToBackup\kek.txt"));

            // Act
            service.CreateRestorePoint(backupJob.Id);

            // Assert
            Assert.AreEqual(1, service.Repository.Storages.Count);
        }
    }
}