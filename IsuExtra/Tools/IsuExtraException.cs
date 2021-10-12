using System;

namespace IsuExtra.Tools
{
    public class IsuExtraException : Exception
    {
        public IsuExtraException(string message)
            : base(message)
        {
        }
    }
}