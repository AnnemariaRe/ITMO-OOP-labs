using System;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class ClassSchedule
    {
        private readonly TimeSpan _start;
        private readonly TimeSpan _end;
        private readonly int _weekDay;

        public ClassSchedule(TimeSpan start, TimeSpan end, int weekDay)
        {
            if (start == null) throw new NullInputException("Invalid start time");
            if (end == null) throw new NullInputException("Invalid end time");
            if (weekDay is 0) throw new NullInputException("Invalid week day");
            _start = start;
            _end = end;
            _weekDay = weekDay;
        }

        private string Start => _start.ToString();
        private string End => _end.ToString();

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType()) return false;

            var c = (ClassSchedule)obj;
            return (Start == c.Start) && (End == c.End) && (_weekDay == c._weekDay);
        }

        public override int GetHashCode() => HashCode.Combine(_start, _end, _weekDay);
    }
}