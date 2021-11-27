using System;
using System.Collections.Generic;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace BackupsExtra.Cleaner
{
    public class CleanerFactory
    {
        private int _limitNumber;
        private DateTime _limitDate;
        private List<ICleaner> _cleaners;
        private string _type;

        public CleanerFactory SetValues(int limitNumber, DateTime limitDate, List<ICleaner> cleaners, string hybridType)
        {
            if (limitNumber <= 0) throw new NullOrEmptyBackupExtraException("Limit number cannot be null or negative");
            if (limitDate == null) throw new NullOrEmptyBackupExtraException("Limit date cannot be null");
            _limitNumber = limitNumber;
            _limitDate = limitDate;
            _cleaners = cleaners ?? throw new NullOrEmptyBackupExtraException("List of cleaners cannot be null");

            _type = hybridType switch
            {
                CleanTypes.AtLeastOne => CleanTypes.AtLeastOne,
                CleanTypes.All => CleanTypes.All,
                _ => throw new BackupExtraException("Invalid hybrid cleaner type")
            };

            return this;
        }

        public ICleaner CreateCleaner(string cleanerType)
        {
            return cleanerType switch
            {
                CleanTypes.Limit => new LimitCleaner(_limitNumber),
                CleanTypes.Date => new DateCleaner(_limitDate),
                CleanTypes.Hybrid => new HybridCleaner(_cleaners, _type),
                _ => throw new NullOrEmptyBackupExtraException("Invalid cleaner type")
            };
        }
    }
}