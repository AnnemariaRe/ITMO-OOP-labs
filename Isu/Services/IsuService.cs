using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Objects;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int MaxStudentsInGroup = 15;
        private const int GroupLength = 5;

        private readonly List<Group> _groups;
        private int _idCounter;

        public IsuService()
        {
            _groups = new List<Group>();
            _idCounter = 0;
        }

        public Group AddGroup(string name)
        {
            if (name.Length != GroupLength || !name.StartsWith("M3")) throw new IsuException("Invalid group name");

            int.TryParse(name.Substring(2, 1), out int course);
            if (course > 4 || course == 0) throw new IsuException("Invalid group name");
            var newGroup = new Group(name, course);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (@group.GetStudents().Count >= MaxStudentsInGroup) throw new IsuException("Too many students in current group");

            _idCounter++;
            @group.AddStudent(name, _idCounter);
            return @group.GetStudent(name);
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _groups.SelectMany(@group => @group.GetStudents().Where(student => student.GetId() == id)))
            {
                return student;
            }

            throw new IsuException("The student with current id doesn't exist");
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _groups.SelectMany(@group => @group.GetStudents().Where(student => student.GetName() == name)))
            {
                return student;
            }

            throw new IsuException("Unable to find the student with current name");
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups.Where(@group => @group.GetName() == groupName))
            {
                return group.GetStudents();
            }

            throw new IsuException("Unable to find students in current group");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _groups.Where(@group => @group.GetCourseNumber() == courseNumber.GetNumber()).SelectMany(@group => @group.GetStudents()).ToList();
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group @group in _groups.Where(@group => @group.GetName() == groupName))
            {
                return group;
            }

            throw new IsuException("Unable to find group with current name");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(@group => @group.GetCourseNumber() == courseNumber.GetNumber()).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.GetName() == student.GetGroupName()) throw new IsuException("Student is already in the current group");
            if (student.GetCourseNumber() != newGroup.GetCourseNumber()) throw new IsuException("Student cannot be transferred to the different course");

            foreach (Group @group in _groups.Where(@group => @group.GetName() == student.GetGroupName()))
            {
                @group.RemoveStudent(student);
            }

            newGroup.AddStudent(student.GetName(), student.GetId());
        }
    }
}