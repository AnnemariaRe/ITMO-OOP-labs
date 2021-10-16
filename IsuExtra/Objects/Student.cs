using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Student
    {
        private const int MinCourseNumber = 1;
        private const int MaxCourseNumber = 4;
        private const int GroupLength = 5;
        private readonly string _name;
        private readonly int _id;
        private int _course;
        private string _group;
        private string _ognpName1;
        private string _ognpName2;

        public Student(string name, int id)
        {
            if (string.IsNullOrEmpty(name)) throw new NullInputException("Invalid student name");
            if (id is 0) throw new NullInputException("Invalid student id");
            _name = name;
            _id = id;
            _group = null;
            _ognpName1 = null;
            _ognpName2 = null;
            _course = 0;
        }

        public string Name => _name;
        public int Id => _id;
        public string Group
        {
            get => _group;
            private set => _group = value;
        }

        public string OgnpName1
        {
            get => _ognpName1;
            private set => _ognpName1 = value;
        }

        public string OgnpName2
        {
            get => _ognpName2;
            private set => _ognpName2 = value;
        }

        public int Course => _course;

        public void AddGroupToStudent(string groupName)
        {
            if (groupName.Length != GroupLength) throw new IsuExtraException("Invalid group name");
            if (!int.TryParse(groupName.Substring(2, 1), out int number)) throw new IsuExtraException("Invalid group name");
            if (number is > MaxCourseNumber or < MinCourseNumber) throw new IsuExtraException("Invalid group name");

            Group = groupName;
            _course = number;
        }

        public void AddOgnpToStudent(string ognpName)
        {
            if (ognpName == null) throw new NullInputException("Invalid ognp name");
            if (OgnpName1 == ognpName || OgnpName2 == ognpName) throw new IsuExtraException("Student is already in the current group");

            if (OgnpName1 == null) OgnpName1 = ognpName;
            else if (OgnpName2 == null) OgnpName2 = ognpName;
            else throw new IsuExtraException("Student is already in 2 ognp groups");
        }

        public void RemoveOgnp(string ognpName)
        {
            if (ognpName == null) throw new NullInputException("Invalid ognp name");

            if (OgnpName1 != null && OgnpName1.StartsWith(ognpName)) OgnpName1 = null;
            else if (OgnpName2 != null && OgnpName2.StartsWith(ognpName)) OgnpName2 = null;
            else throw new IsuExtraException("Student is not in the current group");
        }
    }
}