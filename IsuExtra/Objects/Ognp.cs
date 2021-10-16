using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Ognp
    {
        private readonly string _name;
        private readonly string _facultyName;
        private readonly List<Group> _groups;

        public Ognp(string name, string facultyName)
        {
            if (string.IsNullOrEmpty(name)) throw new NullInputException("Invalid ognp name");
            if (string.IsNullOrEmpty(facultyName)) throw new NullInputException("Invalid faculty name");
            _facultyName = facultyName;
            _groups = new List<Group>();
            _name = name;
        }

        public string Name => _name;
        public string FacultyName => _facultyName;
        public List<Group> Groups => _groups;

        public void AddGroup(string name)
        {
            Groups.Add(new Group(name));
        }

        public Class AddClassToGroup(string groupName, int startHour, int startMinute, int endHour, int endMinute, int weekDay, int audience)
        {
            return Groups.FirstOrDefault(group => group.Name == groupName)?.AddClass(Name, startHour, startMinute, endHour, endMinute, weekDay, audience);
        }
    }
}