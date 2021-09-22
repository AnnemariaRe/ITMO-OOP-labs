using Isu.Objects;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            // Arrange
            var expectedStudentName = "Annemaria Repenko";
            var expectedGroupName = "M3206";
            Group group = _isuService.AddGroup(expectedGroupName);
            
            // Act
            Student student = _isuService.AddStudent(group, expectedStudentName);

            // Assert
            Assert.AreEqual(expectedGroupName, student.Group);
            Assert.AreEqual(expectedStudentName, group.GetStudent(student.Name).Name);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3206");
                _isuService.AddStudent(group, "Victor Odinets");
                _isuService.AddStudent(group, "Ilya Antipin");
                _isuService.AddStudent(group, "Nikolay Kondratev");
                _isuService.AddStudent(group, "Annemaria Repenko");
                _isuService.AddStudent(group, "Nikolay Belyanin");
                _isuService.AddStudent(group, "Andrew Orlov");
                _isuService.AddStudent(group, "Daniil Shevchuk");
                _isuService.AddStudent(group, "Matvey Chernishev");
                _isuService.AddStudent(group, "Kirill Poznyansky");
                _isuService.AddStudent(group, "Oleg Podshivalov");
                _isuService.AddStudent(group, "Igor Kulyaev");
                _isuService.AddStudent(group, "Mikhail Tarasov");
                _isuService.AddStudent(group, "Alexander Bosov");
                _isuService.AddStudent(group, "Konstantin Adreanov");
                _isuService.AddStudent(group, "Alexander Svistunov");
                _isuService.AddStudent(group, "Vladimir Eremchenko");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService = new IsuService();
                _isuService.AddGroup("N2940");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService = new IsuService();
                Group group = _isuService.AddGroup("M3206");
                Group newGroup = _isuService.AddGroup("M3411");
                Student student = _isuService.AddStudent(group, "Annemaria Repenko");
                _isuService.ChangeStudentGroup(student, newGroup);
            });
        }
    }
}