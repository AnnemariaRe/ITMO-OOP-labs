using System;

namespace IsuExtra.Tools
{
    public class NullInputException : IsuExtraException
    {
        public NullInputException(string message)
            : base(message)
        {
        }
    }
}