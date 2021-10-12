using System;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Class
    {
        private static readonly TimeSpan Duration = new TimeSpan(1, 30, 0);

        public Class(string subject, string group, int startHour, int startMinute, int endHour, int endMinute, int weekDay, int audience)
        {
            if (subject == null || group == null || startHour == 0 || endHour == 0 || weekDay == 0 || audience == 0)
                throw new NullInputException();

            var start = new TimeSpan(startHour, startMinute, 0);
            var end = new TimeSpan(endHour, endMinute, 0);
            if (end.Subtract(start) != Duration) throw new IsuExtraException("Incorrect class duration");

            Subject = subject;
            Schedule = new ClassSchedule(start, end, weekDay);
            Group = group;
            Audience = audience;
        }

        public string Subject { get; }
        public string Group { get; }
        public int Audience { get; }
        public ClassSchedule Schedule { get; }
    }
}