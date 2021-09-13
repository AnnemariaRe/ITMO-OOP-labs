using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Objects
{
    public class Group
    {
        private readonly string _name;
        private readonly int _course;
        private readonly List<Student> _students;

        public Group(string name, int number)
        {
            _name = name;
            _students = new List<Student>();
            _course = number;
        }

        public string GetName()
        {
            return _name;
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public int GetCourseNumber()
        {
            return _course;
        }

        public void AddStudent(string studentName, int id)
        {
            var newStudent = new Student(studentName, id);
            _students.Add(newStudent);
            newStudent.AddGroupToStudent(_name);
        }

        public Student GetStudent(string name)
        {
            return _students.FirstOrDefault(student => student.GetName() == name);
        }

        public Student FindStudent(int id)
        {
            return _students.FirstOrDefault(student => student.GetId() == id);
        }

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }
    }
}