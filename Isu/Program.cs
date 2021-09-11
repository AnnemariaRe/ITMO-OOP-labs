using System;
using Isu.Objects;
using Isu.Services;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isu = new IsuService();
            var course1 = new CourseNumber(1);
            var course2 = new CourseNumber(2);

            Group group1 = isu.AddGroup("M3111");
            Group group2 = isu.AddGroup("M3206");
            Group group3 = isu.AddGroup("M3207");
            Group group4 = isu.AddGroup("M3201");
            Group group5 = isu.AddGroup("M3303");

            isu.AddStudent(group1, "Dmitry Ivanov");
            isu.AddStudent(group1, "Ivan Sharshov");
            isu.AddStudent(group2, "Annemaria Repenko");
            isu.AddStudent(group2, "Nikolay Kondratev");
            isu.AddStudent(group2, "Alexander Bosov");
            isu.AddStudent(group2, "Nikolay Belyanin");
            isu.AddStudent(group2, "Mikhail Tarasov");
            isu.AddStudent(group3, "Michail Eremchenko");
            isu.AddStudent(group3, "Stanislav Nesterov");
            isu.AddStudent(group4, "Ksenia Vasyutinskaya");
            isu.AddStudent(group5, "Anatoly Petrov");
            isu.AddStudent(group5, "Darya Ivanova");
            isu.AddStudent(group5, "Pavel Makis");

            Console.WriteLine("------------ISU------------- \n");
            Console.WriteLine("Students from group M3206:");
            foreach (Student student in isu.FindStudents("M3206"))
            {
                Console.WriteLine($"Name: {student.GetName()},  Id: {student.GetId()}");
            }

            Console.WriteLine();
            Console.WriteLine("First course students:");
            foreach (Student student in isu.FindStudents(course1))
            {
                Console.WriteLine($"Name: {student.GetName()},  Course: {student.GetCourseNumber()}");
            }

            Console.WriteLine();
            Console.WriteLine("Second course groups:");
            foreach (Group group in isu.FindGroups(course2))
            {
                Console.Write($"{group.GetName()} ");
            }

            Console.WriteLine("\n");
            Console.WriteLine("Student with id 13:");
            Console.WriteLine($"Name: {isu.GetStudent(13).GetName()}, Id: {isu.GetStudent(13).GetId()}, " +
                              $"Course: {isu.GetStudent(13).GetCourseNumber()}, Group: {isu.GetStudent(13).GetGroupName()} \n");

            Console.WriteLine($"Name: {isu.GetStudent(3).GetName()}, Group: {isu.GetStudent(3).GetGroupName()}");
            Console.WriteLine("Changing group....");
            isu.ChangeStudentGroup(isu.GetStudent(3), isu.FindGroup("M3201"));
            Console.WriteLine($"Name: {isu.GetStudent(3).GetName()}, Group: {isu.GetStudent(3).GetGroupName()}");
        }
    }
}
