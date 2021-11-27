using System.Collections.Generic;
using System.IO;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;
using ZipFile = Ionic.Zip.ZipFile;

namespace BackupsExtra.Models
{
    public class SplitStoragesAlgorithm : IAlgorithm
    {
        public IEnumerable<Storage> SaveCopies(List<JobObject> jobObjects)
        {
            if (jobObjects is null) throw new NullOrEmptyBackupExtraException("Job objects cannot be null");

            var storages = new List<Storage>();
            foreach (var jobObject in jobObjects)
            {
                var zip = new ZipFile();
                zip.AddFile(jobObject.File.FullName);
                storages.Add(new Storage(Path.GetFileNameWithoutExtension(jobObject.File.Name) + $".zip", zip));
            }

            return storages;
        }
    }
}