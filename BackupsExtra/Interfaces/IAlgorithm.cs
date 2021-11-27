using System.Collections.Generic;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface IAlgorithm
    {
        public IEnumerable<Storage> SaveCopies(List<JobObject> jobObjects);
    }
}