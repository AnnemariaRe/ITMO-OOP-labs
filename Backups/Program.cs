using Backups.Models;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var service = new BackupService();
            var backupJob = service.CreateBackupJob("SingleStorage");
            service.AddJobObject(backupJob.Id, new JobObject(@"C:\Users\PC\source\repos\AnnemariaRe\Backups\FilesToBackup\aboba.txt"));
            service.AddJobObject(backupJob.Id, new JobObject(@"C:\Users\PC\source\repos\AnnemariaRe\Backups\FilesToBackup\bebra.txt"));

            service.CreateRestorePoint(backupJob.Id);

            service.SaveBackup();
        }
    }
}
