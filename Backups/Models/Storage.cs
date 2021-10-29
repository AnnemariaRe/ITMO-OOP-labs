using Backups.Tools;
using ZipFile = Ionic.Zip.ZipFile;

namespace Backups.Models
{
    public class Storage
    {
        public Storage(string storageName, ZipFile zip)
        {
            if (string.IsNullOrEmpty(storageName)) throw new NullOrEmptyBackupException(" cannot be null");
            if (zip is null) throw new NullOrEmptyBackupException("Zip file cannot be null");
            StorageName = storageName;
            Zip = zip;
        }

        public string StorageName { get; }
        public ZipFile Zip { get; }
    }
}