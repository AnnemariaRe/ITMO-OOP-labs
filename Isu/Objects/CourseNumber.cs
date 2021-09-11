namespace Isu.Objects
{
    public class CourseNumber
    {
        private readonly int _number;

        public CourseNumber(int number)
        {
            _number = number;
        }

        public int GetNumber()
        {
            return _number;
        }
    }
}