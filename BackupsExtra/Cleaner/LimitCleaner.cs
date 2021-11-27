using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Cleaner
{
    public class LimitCleaner : ICleaner
    {
        private readonly int _limitNumber;

        public LimitCleaner(int limitNumber)
        {
            _limitNumber = limitNumber;
        }

        public int CalculatePointsToDelete(List<RestorePoint> points)
        {
            if (points == null) throw new NullOrEmptyBackupExtraException("List of restore points cannot be null");
            int toDelete = points.Count - _limitNumber;
            return toDelete;
        }
    }
}