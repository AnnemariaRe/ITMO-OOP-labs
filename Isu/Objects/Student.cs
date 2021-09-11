namespace Isu.Objects
{
    public class Student
    {
        private const int CourseIndex = 2;

        private readonly string _name;
        private readonly int _id;
        private string _group;
        private int _course;

        public Student(string name, int id)
        {
            _name = name;
            _group = null;
            _id = id;
            _course = 0;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetGroupName()
        {
            return _group;
        }

        public void AddGroupToStudent(string groupName)
        {
            _group = groupName;
            _course = _group[CourseIndex] - '0';
        }

        public int GetCourseNumber()
        {
            return _course;
        }
    }
}