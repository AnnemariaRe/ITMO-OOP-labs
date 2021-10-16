using System;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Class
    {
        private static readonly TimeSpan Duration = new TimeSpan(1, 30, 0);
        private string _subject;
        private string _group;
        private int _audience;
        private ClassSchedule _schedule;

        public Class(string subject, string group, int startHour, int startMinute, int endHour, int endMinute, int weekDay, int audience)
        {
            if (string.IsNullOrEmpty(subject)) throw new NullInputException("Invalid subject name");
            if (string.IsNullOrEmpty(group)) throw new NullInputException("Invalid group name");
            if (startHour is 0) throw new NullInputException("Invalid start hour");
            if (endHour is 0) throw new NullInputException("Invalid end hour");
            if (weekDay is 0) throw new NullInputException("Invalid week day");
            if (audience is 0) throw new NullInputException("Invalid audience number");

            var start = new TimeSpan(startHour, startMinute, 0);
            var end = new TimeSpan(endHour, endMinute, 0);
            if (end.Subtract(start) != Duration) throw new IsuExtraException("Incorrect class duration");

            _subject = subject;
            _schedule = new ClassSchedule(start, end, weekDay);
            _group = group;
            _audience = audience;
        }

        public ClassSchedule Schedule => _schedule;
    }
}