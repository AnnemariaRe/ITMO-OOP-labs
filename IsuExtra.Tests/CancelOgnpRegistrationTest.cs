using System;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class CancelOgnpRegistrationTest
    {
        private IsuExtraService isu; 
        
        [SetUp]
        public void Setup()
        {
            isu = new IsuExtraService();
        }

        [Test]
        public void CancelOgnpRegistration()
        {
            var group = isu.AddGroup("M3206");
            isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var student = isu.AddStudent(group, "Annemaria Repenko");
            var ognp = isu.AddOgnp("CyberSecurity", "N3", 1);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 8, 20, 9, 50, 5, 245);
            isu.AddStudentToOgnp(student, ognp);
            
            // Act
            isu.CancelRegistration(student, ognp);
            
            // Assert
            Assert.AreEqual(null, student.OgnpName1);
            Assert.AreEqual(false, ognp.Groups[0].Students.Contains(student));
        }
        
    }
}