using System.Collections.Generic;
using Backups.Models;

namespace Backups.Interfaces
{
    public interface IAlgorithm
    {
        public List<Storage> SaveCopies(int number, List<JobObject> jobObjects);
    }
}