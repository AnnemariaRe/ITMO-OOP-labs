using System;
using System.Text;
using BackupsExtra.Tools;
using ZipFile = Ionic.Zip.ZipFile;

namespace BackupsExtra.Models
{
    [Serializable]
    public class Storage
    {
        public Storage(string storageName, ZipFile zip)
        {
            if (string.IsNullOrEmpty(storageName)) throw new NullOrEmptyBackupExtraException(" cannot be null");
            StorageName = storageName;
            Zip = zip ?? throw new NullOrEmptyBackupExtraException("Zip file cannot be null");
        }

        public string StorageName { get; }
        public ZipFile Zip { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Storage name: {StorageName}, Zip file name: {Zip.Name}");
            return sb.ToString();
        }
    }
}