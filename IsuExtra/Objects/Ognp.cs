using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Ognp
    {
        public Ognp(string name, string facultyName)
        {
            if (name == null || facultyName == null) throw new NullInputException();
            Name = name;
            FacultyName = facultyName;
            Groups = new List<Group>();
        }

        public string Name { get; }
        public string FacultyName { get; }
        public List<Group> Groups { get; }

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