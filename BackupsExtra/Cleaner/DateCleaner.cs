using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Cleaner
{
    public class DateCleaner : ICleaner
    {
        private readonly DateTime _limitDate;

        public DateCleaner(DateTime date)
        {
            _limitDate = date;
        }

        public int CalculatePointsToDelete(List<RestorePoint> points)
        {
            if (points == null) throw new NullOrEmptyBackupExtraException("List of restore points cannot be null");
            return points.Count(point => point.CreationTime < _limitDate);
        }
    }
}