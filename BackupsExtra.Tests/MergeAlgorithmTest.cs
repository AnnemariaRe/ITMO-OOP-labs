using System.IO;
using BackupsExtra.Models;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class MergeAlgorithmTest
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
        public void MergeTest()
        {
            int limitValue = 1;
            var job = _service.CreateBackupJob("Split", "Limit", default, limitValue);

            _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "bebra.txt"));
            var obj1 = _service.AddJobObject(job.Id,
                new JobObject(_service.Repository.FullPath + Path.DirectorySeparatorChar + "aboba.txt"));

            _service.CreateRestorePoint(job.Id);
            _service.RemoveJobObject(job.Id, obj1);
            var point = _service.CreateRestorePoint(job.Id);

            Assert.AreEqual(2, point.Storages.Count);
        }
    }
}