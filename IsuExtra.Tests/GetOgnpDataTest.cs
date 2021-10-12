using System.Collections.Generic;
using IsuExtra.Objects;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class GetOgnpDataTest
    {
        private IsuExtraService isu; 
        
        [SetUp]
        public void Setup()
        {
            isu = new IsuExtraService();
        }

        [Test]
        public void GetOgnpGroups()
        {
            // Arrange 
            var ognp = isu.AddOgnp("CyberSecurity", "N3", 3);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 8, 20, 9, 50, 5, 245);

            // Act
            var result = isu.GetOgnpGroups(ognp.Name);

            // Assert
            Assert.AreEqual(ognp.Groups, result);
        }
        
        [Test]
        public void GetStudentsFromOgnp()
        {
            // Arrange 
            var group = isu.AddGroup("M3206");
            isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var students = new List<Student>();
            students.Add(isu.AddStudent(group, "Annemaria Repenko"));
            students.Add(isu.AddStudent(group, "Andrew Petrov"));
            students.Add(isu.AddStudent(group, "Anna Ivanova"));
            var ognp = isu.AddOgnp("CyberSecurity", "N3", 1);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 8, 20, 9, 50, 5, 245);
            isu.AddStudentToOgnp(students[0], ognp);
            isu.AddStudentToOgnp(students[1], ognp);
            isu.AddStudentToOgnp(students[2], ognp);

            // Act
            var result = isu.GetStudentsFromOgnp(ognp.Groups[0].Name);

            // Assert
            Assert.AreEqual(students, result);
        }
        
        [Test]
        public void GetStudentsWithoutOgnp()
        {
            // Arrange 
            var group = isu.AddGroup("M3206");
            isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var students = new List<Student>();
            var student = (isu.AddStudent(group, "Annemaria Repenko"));
            students.Add(isu.AddStudent(group, "Andrew Petrov"));
            students.Add(isu.AddStudent(group, "Anna Ivanova"));
            var ognp = isu.AddOgnp("CyberSecurity", "N3", 1);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 8, 20, 9, 50, 5, 245);
            isu.AddStudentToOgnp(student, ognp);

            // Act
            var result = isu.GetStudentsWithoutOgnp(group.Name);

            // Assert
            Assert.AreEqual(students, result);
        }
    }
}