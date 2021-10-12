using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Group
    {
        private static readonly TimeSpan Duration = new TimeSpan(1, 30, 0);
        public Group(string name, int number)
        {
            if (name == null || number == 0) throw new NullInputException();
            Name = name;
            Course = number;
            Students = new List<Student>();
            Classes = new List<Class>();
        }

        public Group(string name)
        {
            if (name == null) throw new NullInputException();
            Name = name;
            int.TryParse(name.Substring(2, 1), out int course);
            Course = course;
            Students = new List<Student>();
            Classes = new List<Class>();
        }

        public string Name { get; }
        public int Course { get; }
        public List<Student> Students { get; }
        public List<Class> Classes { get; }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            if (Course != 0) student.AddGroupToStudent(Name);
            else student.AddOgnpToStudent(Name);
        }

        public Student GetStudent(string name)
        {
            return Students.FirstOrDefault(student => student.Name == name);
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
            if (startHour is < 8 or > 20 || endHour is < 9 or > 21) throw new IsuExtraException("Incorrect time");
            if (startMinute is < 0 or > 50 || endMinute is < 0 or > 50) throw new IsuExtraException("Incorrect time");
            var start = new TimeSpan(startHour, startMinute, 0);
            var end = new TimeSpan(endHour, endMinute, 0);
            if (end.Subtract(start) != Duration) throw new IsuExtraException("Incorrect class duration");
            if (weekDay is > 6 or < 1) throw new IsuExtraException("Incorrect day of the week");

            var cl = new Class(subject, Name, startHour, startMinute, endHour, endMinute, weekDay, audience);
            Classes.Add(cl);
            return cl;
        }
    }
}