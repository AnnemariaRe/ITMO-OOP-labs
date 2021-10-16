using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Group
    {
        private const int MinStartHour = 8;
        private const int MaxStartHour = 20;
        private const int MinEndHour = 9;
        private const int MaxEndHour = 21;
        private const int MaxMinute = 50;
        private const int MinMinute = 0;
        private const int MinWeekDay = 1;
        private const int MaxWeekDay = 7;

        private static readonly TimeSpan Duration = new TimeSpan(1, 30, 0);
        private readonly string _name;
        private readonly int _course;
        private readonly List<Student> _students;
        private readonly List<Class> _classes;

        public Group(string name, int number)
        {
            if (string.IsNullOrEmpty(name)) throw new NullInputException("Invalid group name");
            if (number is 0) throw new NullInputException("Invalid course number");
            _name = name;
            _course = number;
            _students = new List<Student>();
            _classes = new List<Class>();
        }

        public Group(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new NullInputException("Invalid group name");
            _name = name;
            int.TryParse(name.Substring(2, 1), out int course);
            _course = course;
            _students = new List<Student>();
            _classes = new List<Class>();
        }

        public string Name => _name;
        public int Course => _course;
        public List<Student> Students => _students;
        public List<Class> Classes => _classes;

        public void AddStudent(Student student)
        {
            Students.Add(student);
            if (Course != 0) student.AddGroupToStudent(Name);
            else student.AddOgnpToStudent(Name);
        }

        public Student GetStudent(string name)
        {
            if (Students.Any(student => student.Name == name))
                return Students.FirstOrDefault(student => student.Name == name);
            throw new IsuExtraException("Student is not in the current group");
        }

        public Student FindStudent(int id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        public Class AddClass(string subject, int startHour, int startMinute, int endHour, int endMinute, int weekDay, int audience)
        {
            if (startHour is < MinStartHour or > MaxStartHour || endHour is < MinEndHour or > MaxEndHour) throw new IsuExtraException("Incorrect time");
            if (startMinute is < MinMinute or > MaxMinute || endMinute is < MinMinute or > MaxMinute) throw new IsuExtraException("Incorrect time");
            var start = new TimeSpan(startHour, startMinute, 0);
            var end = new TimeSpan(endHour, endMinute, 0);
            if (end.Subtract(start) != Duration) throw new IsuExtraException("Incorrect class duration");
            if (weekDay is > MaxWeekDay or < MinWeekDay) throw new IsuExtraException("Incorrect day of the week");

            var cl = new Class(subject, Name, startHour, startMinute, endHour, endMinute, weekDay, audience);
            Classes.Add(cl);
            return cl;
        }
    }
}