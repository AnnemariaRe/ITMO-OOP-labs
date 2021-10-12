using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Objects;
using IsuExtra.Tools;
using Group = IsuExtra.Objects.Group;
using Student = IsuExtra.Objects.Student;

namespace IsuExtra.Services
{
    public class IsuExtraService
    {
        private const int MaxStudentsInGroup = 15;
        private const int GroupLength = 5;
        private const int MaxStudentsInOgnp = 20;

        private readonly List<Group> _groups;
        private readonly List<Ognp> _ognp;
        private int _idCounter;

        public IsuExtraService()
        {
            _ognp = new List<Ognp>();
            _groups = new List<Group>();
            _idCounter = 0;
        }

        public Group AddGroup(string name)
        {
            if (name.Length != GroupLength) throw new IsuExtraException("Invalid group name");

            if (!char.TryParse(name.Substring(0, 1), out char letter)) throw new IsuExtraException("Invalid group name");
            if (!int.TryParse(name.Substring(2, 1), out int course)) throw new IsuExtraException("Invalid group name");
            if (course is > 4 or 0) throw new IsuExtraException("Invalid group name");
            var newGroup = new Group(name, course);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= MaxStudentsInGroup) throw new IsuExtraException("Too many students in current group");

            _idCounter++;
            group.AddStudent(new Student(name, _idCounter));
            return group.GetStudent(name);
        }

        public Student GetStudent(int id)
        {
            return _groups.SelectMany(group =>
                group.Students.Where(student => student.Id == id)).FirstOrDefault();
        }

        public Student FindStudent(string name)
        {
            return _groups.SelectMany(group => group.Students.Where(student => student.Name == name))
                .FirstOrDefault();
        }

        public List<Student> FindStudents(string groupName)
        {
            return _groups.FirstOrDefault(group => group.Name == groupName)?.Students;
        }

        public List<Student> FindStudents(int courseNumber)
        {
            return _groups.Where(group => group.Course == courseNumber).SelectMany(group => group.Students).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(group => group.Name == groupName);
        }

        public List<Group> FindGroups(int courseNumber)
        {
            return _groups.Where(group => group.Course == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student == null || newGroup == null) throw new NullInputException();
            if (newGroup.Name == student.Group) throw new IsuExtraException("Student is already in the current group");
            if (student.Course != newGroup.Course && (student.Course + 1) != newGroup.Course) throw new IsuExtraException("Student cannot be transferred to required course");

            foreach (Group group in _groups.Where(group => group.Name == student.Group))
            {
                group.RemoveStudent(student);
            }

            newGroup.AddStudent(student);
        }

        // IsuExtra
        public Class AddClassToGroup(string subject, string group, int startHour, int startMinute, int endHour, int endMinute, int weekDay, int audience)
        {
            if (subject == null || group == null || audience == 0) throw new NullInputException();
            return _groups.FirstOrDefault(gr => gr.Name == group)?.AddClass(subject, startHour, startMinute, endHour, endMinute, weekDay, audience);
        }

        public Class AddClassToOgnpGroup(string group, int startHour, int startMinute, int endHour, int endMinute, int weekDay, int audience)
        {
            if (group == null || audience == 0) throw new NullInputException();
            return _ognp.FirstOrDefault(o => o.Groups.Any(g => group.StartsWith(g.Name)))
                ?.AddClassToGroup(group, startHour, startMinute, endHour, endMinute, weekDay, audience);
        }

        public Ognp AddOgnp(string name, string faculty, int numberOfGroups)
        {
            if (name == null || faculty == null || numberOfGroups is < 1 or > 5) throw new NullInputException();
            if (!int.TryParse(faculty.Substring(1, 1), out int number) || faculty.Length != 2) throw new IsuExtraException("Invalid faculty name");
            if (!char.TryParse(faculty.Substring(0, 1), out char letter)) throw new IsuExtraException("Invalid faculty name");

            var ognp = new Ognp(name, faculty);
            for (int i = 1; i <= numberOfGroups; i++) ognp.AddGroup($"{name}{i}");

            _ognp.Add(ognp);
            return ognp;
        }

        public void AddStudentToOgnp(Student student, Ognp ognp)
        {
            if (student == null || ognp == null) throw new NullInputException();
            if (student.Group.StartsWith(ognp.FacultyName))
                throw new IsuExtraException("Student cannot be added to the ognp of his faculty");
            if (!_ognp.Contains(ognp)) throw new IsuExtraException("Current ognp doesn't exist");
            if (student.OgnpName1 != null && student.OgnpName2 != null)
                throw new IsuExtraException("Student already has ognp");

            bool check;
            foreach (var ognpGroup in ognp.Groups)
            {
                check = true;
                if (ognpGroup.Students.Count > MaxStudentsInOgnp ||
                    ognpGroup.Classes.Any(ognpClass => _groups.First(group => group.Name == student.Group)
                        .Classes.Any(groupClass => ognpClass.Schedule.Equals(groupClass.Schedule))))
                {
                    check = false;
                }

                if (check)
                {
                    ognpGroup.AddStudent(student);
                    break;
                }
            }

            if (!ognp.Groups.Any(group => group.Students.Contains(student)))
                throw new IsuExtraException("Cannot find suitable ognp group for the student");
        }

        public void CancelRegistration(Student student, Ognp ognp)
        {
            if (student == null || ognp == null) throw new NullInputException();
            if (_ognp.Contains(ognp) && ognp.Groups.Any(group => group.Students.Contains(student)))
            {
                ognp.Groups.FirstOrDefault(group => group.Students.Contains(student))?.Students.Remove(student);
            }

            student.RemoveOgnp(ognp.Name);
        }

        public List<Student> GetStudentsFromOgnp(string ognpName)
        {
            if (ognpName == null) throw new NullInputException();
            return _ognp.FirstOrDefault(ognp => ognpName.StartsWith(ognp.Name))?.Groups
                .FirstOrDefault(group => group.Name == ognpName)?.Students;
        }

        public List<Student> GetStudentsWithoutOgnp(string groupName)
        {
            if (groupName == null) throw new NullInputException();
            return _groups.FirstOrDefault(group => group.Name == groupName)
                ?.Students.Where(student => student.OgnpName1 == null && student.OgnpName2 == null).ToList();
        }

        public List<Group> GetOgnpGroups(string ognpName)
        {
            if (ognpName == null) throw new NullInputException();
            return _ognp.FirstOrDefault(o => o.Name == ognpName)?.Groups;
        }
    }
}