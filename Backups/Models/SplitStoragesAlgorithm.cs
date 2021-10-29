using System.Collections.Generic;
using Backups.Interfaces;
using Backups.Tools;
using ZipFile = Ionic.Zip.ZipFile;

namespace Backups.Models
{
    public class SplitStoragesAlgorithm : IAlgorithm
    {
        public List<Storage> SaveCopies(int number, List<JobObject> jobObjects)
        {
            if (number is 0) throw new NullOrEmptyBackupException("Number cannot ba null");
            if (jobObjects is null) throw new NullOrEmptyBackupException("Job objects cannot be null");

            var storages = new List<Storage>();
            foreach (var jobObject in jobObjects)
            {
                var zip = new ZipFile();
                zip.AddFile(jobObject.File.FullName);
                storages.Add(new Storage(jobObject.File.Name.Substring(0, jobObject.File.Name.LastIndexOf(".")) + $"_{number}.zip", zip));
            }

            return storages;
        }
    }
}