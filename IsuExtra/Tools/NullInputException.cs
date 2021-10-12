using System;

namespace IsuExtra.Tools
{
    public class NullInputException : Exception
    {
        public NullInputException(string message = "Invalid input name")
            : base(message)
        {
        }
    }
}