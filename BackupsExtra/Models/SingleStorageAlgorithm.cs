using System.Collections.Generic;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;
using Ionic.Zip;

namespace BackupsExtra.Models
{
    public class SingleStorageAlgorithm : IAlgorithm
    {
        public IEnumerable<Storage> SaveCopies(List<JobObject> jobObjects)
        {
            if (jobObjects is null) throw new NullOrEmptyBackupExtraException("Job objects cannot be null");

            var zip = new ZipFile();
            foreach (var jobObject in jobObjects) zip.AddFile(jobObject.File.FullName);

            return new List<Storage>() { new Storage($"Archive.zip", zip) };
        }
    }
}