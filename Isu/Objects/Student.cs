using Isu.Tools;

namespace Isu.Objects
{
    public class Student
    {
        private const int GroupLength = 5;
        private int course;

        public Student(string name, int id)
        {
            Name = name;
            Group = null;
            Id = id;
            course = 0;
        }

        public string Name { get; }
        public int Id { get; }
        public string Group { get; set; }

        public int Course
        {
            get => course;
        }

        public void AddGroupToStudent(string groupName)
        {
            if (groupName.Length != GroupLength || !groupName.StartsWith("M3")) throw new IsuException("Invalid group name");
            if (!int.TryParse(groupName.Substring(2, 1), out int course)) throw new IsuException("Invalid group name");
            if (course > 4 || course == 0) throw new IsuException("Invalid group name");

            Group = groupName;
            int.TryParse(groupName.Substring(2, 1), out course);
        }
    }
}