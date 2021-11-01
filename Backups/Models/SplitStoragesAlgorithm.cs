using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;
using Backups.Tools;
using ZipFile = Ionic.Zip.ZipFile;

namespace Backups.Models
{
    public class SplitStoragesAlgorithm : IAlgorithm
    {
        public List<Storage> SaveCopies(int number, List<JobObject> jobObjects)
        {
            if (number is 0) throw new NullOrEmptyBackupException("Number cannot be null");
            if (jobObjects is null) throw new NullOrEmptyBackupException("Job objects cannot be null");

            var storages = new List<Storage>();
            foreach (var jobObject in jobObjects)
            {
                var zip = new ZipFile();
                zip.AddFile(jobObject.File.FullName);
                storages.Add(new Storage(Path.GetFileNameWithoutExtension(jobObject.File.Name) + $"_{number}.zip", zip));
            }

            return storages;
        }
    }
}