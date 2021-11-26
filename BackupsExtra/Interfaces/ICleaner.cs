using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface ICleaner
    {
        public List<RestorePoint> Clean(List<RestorePoint> points)
        {
            int toDelete = CalculatePointsToDelete(points);
            return DeletePoints(toDelete, points);
        }

        public int CalculatePointsToDelete(List<RestorePoint> points);
        protected List<RestorePoint> DeletePoints(int toDelete, List<RestorePoint> points)
        {
            if (points.Count == toDelete) return new List<RestorePoint>();

            for (int i = 1; i <= toDelete; i++)
            {
                if (points.Count < toDelete) break;

                if (points[i - 1].StorageType == StorageTypes.SingleStorage)
                {
                    points.Remove(points[i - 1]);
                    continue;
                }

                foreach (var storage in points[i - 1].Storages.Where(storage => points[i].Storages.All(s => s.StorageName != storage.StorageName)))
                {
                    points[i].Storages.Add(storage);
                }

                points.Remove(points[i - 1]);
            }

            return points;
        }
    }
}