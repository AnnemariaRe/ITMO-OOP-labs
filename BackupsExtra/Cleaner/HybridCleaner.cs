using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Cleaner
{
    public class HybridCleaner : ICleaner
    {
        private readonly List<ICleaner> _cleaners;
        private readonly string _type;

        public HybridCleaner(List<ICleaner> cleaners, string hybridType)
        {
            _cleaners = cleaners;
            _type = hybridType;
        }

        public int CalculatePointsToDelete(List<RestorePoint> points)
        {
            if (points == null) throw new NullOrEmptyBackupExtraException("List of restore points cannot be null");
            return _type switch
            {
                CleanTypes.AtLeastOne => _cleaners.Max(cleaner => cleaner.CalculatePointsToDelete(points)),
                CleanTypes.All => _cleaners.Min(cleaner => cleaner.CalculatePointsToDelete(points)),
                _ => throw new BackupExtraException("Cannot calculate points to delete")
            };
        }
    }
}