using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Student
    {
        private const int GroupLength = 5;
        private int course;

        public Student(string name, int id)
        {
            if (name == null || id == 0) throw new NullInputException();
            Name = name;
            Id = id;
            Group = null;
            OgnpName1 = null;
            OgnpName2 = null;
            course = 0;
        }

        public string Name { get; }
        public int Id { get; }
        public string Group { get; private set; }

        public string OgnpName1 { get; private set; }
        public string OgnpName2 { get; private set; }

        public int Course
        {
            get => course;
        }

        public void AddGroupToStudent(string groupName)
        {
            if (groupName.Length != GroupLength) throw new IsuExtraException("Invalid group name");
            if (!int.TryParse(groupName.Substring(2, 1), out int course)) throw new IsuExtraException("Invalid group name");
            if (course is > 4 or 0) throw new IsuExtraException("Invalid group name");

            Group = groupName;
            int.TryParse(groupName.Substring(2, 1), out course);
        }

        public void AddOgnpToStudent(string ognpName)
        {
            if (ognpName == null) throw new NullInputException();
            if (OgnpName1 == ognpName || OgnpName2 == ognpName) throw new IsuExtraException("Student is already in the current group");

            if (OgnpName1 == null) OgnpName1 = ognpName;
            else if (OgnpName2 == null) OgnpName2 = ognpName;
            else throw new IsuExtraException("Student is already in 2 ognp groups");
        }

        public void RemoveOgnp(string ognpName)
        {
            if (ognpName == null) throw new NullInputException();

            if (OgnpName1 != null && OgnpName1.StartsWith(ognpName)) OgnpName1 = null;
            else if (OgnpName2 != null && OgnpName2.StartsWith(ognpName)) OgnpName2 = null;
            else throw new IsuExtraException("Student is not in the current group");
        }
    }
}