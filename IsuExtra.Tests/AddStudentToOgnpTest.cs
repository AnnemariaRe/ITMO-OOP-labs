using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class AddStudentToOgnpTest
    {
        private IsuExtraService isu; 
        
        [SetUp]
        public void Setup()
        {
            isu = new IsuExtraService();
        }

        [Test]
        public void AddStudentToOgnp()
        {
            // Arrange
            var group = isu.AddGroup("M3206");
            isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var student = isu.AddStudent(group, "Annemaria Repenko");
            var ognp = isu.AddOgnp("CyberSecurity", "N3", 2);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 8, 20, 9, 50, 5, 245);
            isu.AddClassToOgnpGroup(ognp.Groups[1].Name, 13, 30, 15, 0, 4, 225);
            
            // Act
            isu.AddStudentToOgnp(student, ognp);
            
            // Assert
            Assert.AreEqual(ognp.Groups[0].Name, student.OgnpName1);
        }

        [Test]
        public void TryAddStudentToHisFacultyOgnp()
        {
            var group = isu.AddGroup("M3206");
            isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var student = isu.AddStudent(group, "Annemaria Repenko");
            var ognp = isu.AddOgnp("Machine Learning", "M3", 2);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 8, 20, 9, 50, 5, 245);

            Assert.Catch<IsuExtraException>( () =>
            {
                isu.AddStudentToOgnp(student, ognp);
            });
        }

        [Test]
        public void TryAddStudentToOgnpWithUnsuitableSchedule()
        {
            var group = isu.AddGroup("M3206");
            isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var student = isu.AddStudent(group, "Annemaria Repenko");
            var ognp = isu.AddOgnp("CyberSecurity", "N3", 1);
            isu.AddClassToOgnpGroup(ognp.Groups[0].Name, 10, 0, 11, 30, 2, 245);

            Assert.Catch<IsuExtraException>( () =>
            {
                isu.AddStudentToOgnp(student, ognp);
            });
        }

        [Test]
        public void StudentHasOgnpCoursesAlready()
        {
            var group = isu.AddGroup("M3206");
            var class1 = isu.AddClassToGroup("OOP", group.Name, 10, 0, 11, 30, 2, 336);
            var student = isu.AddStudent(group, "Annemaria Repenko");
            var ognp1 = isu.AddOgnp("CyberSecurity", "N3", 1);
            var ognp2 = isu.AddOgnp("Software Methods and Tools", "N3", 1);
            isu.AddClassToOgnpGroup(ognp1.Groups[0].Name, 10, 0, 11, 30, 5, 245);
            isu.AddClassToOgnpGroup(ognp2.Groups[0].Name, 10, 0, 11, 30, 6, 245);
            isu.AddStudentToOgnp(student, ognp1);
            isu.AddStudentToOgnp(student, ognp2);
            var ognp3 = isu.AddOgnp("Physical Optics", "P3", 1);

            Assert.Catch<IsuExtraException>( () =>
            {
                isu.AddStudentToOgnp(student, ognp3);
            });
        }
    }
}