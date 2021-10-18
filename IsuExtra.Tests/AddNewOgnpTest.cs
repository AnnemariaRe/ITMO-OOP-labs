using IsuExtra.Objects;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class AddNewOgnpTest
    {
        private IsuExtraService isu; 
        
        [SetUp]
        public void Setup()
        {
            isu = new IsuExtraService();
        }

        [Test]
        public void AddPossibleOgnp()
        {
            // Arrange
            var name = "CyberSecurity";
            var faculty = "N3";
            var group1name = "CyberSecurity1";
            
            // Act
            var ognp = isu.AddOgnp(name, faculty, 3);
            
            // Assert
            Assert.AreEqual(name, ognp.Name);
            Assert.AreEqual(group1name, ognp.Groups[0].Name);
            Assert.AreEqual(3, ognp.Groups.Count);
        }

        [Test]
        public void AddOgnpWithInvalidFacultyThrowException()
        {
            var name = "CyberSecurity";
            var faculty = "SK1";
            var group1name = "CyberSecurity1";
            
            Assert.Catch<IsuExtraException>(() =>
            {
                isu.AddOgnp(name, faculty, 3);
            });
        }
    }
}