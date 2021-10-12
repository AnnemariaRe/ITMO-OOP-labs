using System;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class ClassSchedule
    {
        private readonly TimeSpan start;
        private readonly TimeSpan end;

        public ClassSchedule(TimeSpan start, TimeSpan end, int weekDay)
        {
            if (start == null || end == null || weekDay == 0) throw new NullInputException();
            this.start = start;
            this.end = end;
            WeekDay = weekDay;
        }

        public string Start => start.ToString();
        public string End => end.ToString();
        public int WeekDay { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType()) return false;

            var c = (ClassSchedule)obj;
            return (Start == c.Start) && (End == c.End) && (WeekDay == c.WeekDay);
        }

        public override int GetHashCode() => HashCode.Combine(start, end, WeekDay);
    }
}