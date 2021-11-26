using System;
using System.IO;
using NUnit.Framework;
using BackupsExtra;
using BackupsExtra.Models;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class CleanerAlgorithmTest
    {
        private BackupService _service; 
        
        [SetUp]
        public void Setup()
        {
            _service = new BackupService();
            File.Create(_service.Repository.FullPath + Path.DirectorySeparatorChar + "bebra.txt").Close();
            File.Create(_service.Repository.FullPath + Path.DirectorySeparatorChar + "aboba.txt").Close();
            File.Create(_service.Repository.FullPath + Path.DirectorySeparatorChar + "kek.txt").Close();
        }

        [Test]
        public void LimitCleanerTest()
        {
            int limitValue = 2;
            var job = _service.CreateBackupJob("Split", "Limit", default, limitValue);
            
            _service.CreateRestorePoint(job.Id);
            _service.CreateRestorePoint(job.Id);
            _service.CreateRestorePoint(job.Id);
            
            Assert.AreEqual(limitValue, job.RestorePoints.Count);
        }

        [Test]
        public void DateCleanerTest()
        {
            DateTime date = DateTime.Now.AddHours(2);
            var job = _service.CreateBackupJob("Split", "Date", date);
            
            _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "bebra.txt"));
            var obj1 = _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "aboba.txt"));
            var obj2 = _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "kek.txt"));
            
            _service.CreateRestorePoint(job.Id);
            _service.RemoveJobObject(job.Id, obj1);
            _service.CreateRestorePoint(job.Id);
            _service.CreateRestorePoint(job.Id);

            Assert.AreEqual(0, job.RestorePoints.Count);
        }

        [Test]
        public void HybridAllCleanerTest()
        {
            DateTime date = DateTime.Now.AddHours(2);
            int limit = 2;
            var job = _service.CreateBackupJob("Split", "Hybrid", date, limit, "All");
            
            _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "bebra.txt"));
            var obj1 = _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "aboba.txt"));

            _service.CreateRestorePoint(job.Id);
            _service.RemoveJobObject(job.Id, obj1);
            _service.CreateRestorePoint(job.Id);
            _service.CreateRestorePoint(job.Id);

            Assert.AreEqual(2, job.RestorePoints.Count);
        }
    }
}