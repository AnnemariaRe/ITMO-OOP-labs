using System.Collections.Generic;
using Backups.Interfaces;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Models
{
    public class SingleStorageAlgorithm : IAlgorithm
    {
        public List<Storage> SaveCopies(int number, List<JobObject> jobObjects)
        {
            if (number is 0) throw new NullOrEmptyBackupException("Number cannot ba null");
            if (jobObjects is null) throw new NullOrEmptyBackupException("Job objects cannot be null");

            var zip = new ZipFile();
            foreach (var jobObject in jobObjects) zip.AddFile(jobObject.File.FullName);

            return new List<Storage>() { new Storage($"Archive_{number}.zip", zip) };
        }
    }
}