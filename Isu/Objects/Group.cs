using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Objects
{
    public class Group
    {
        public Group(string name, int number)
        {
            Name = name;
            Students = new List<Student>();
            Course = number;
        }

        public string Name { get; }
        public int Course { get; }
        public List<Student> Students { get; }

        public void AddStudent(string studentName, int id)
        {
            var newStudent = new Student(studentName, id);
            Students.Add(newStudent);
            newStudent.AddGroupToStudent(Name);
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
    }
}