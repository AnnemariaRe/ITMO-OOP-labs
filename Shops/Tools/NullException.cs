using System;

namespace Shops.Tools
{
    public class NullException : Exception
    {
        public NullException(string message = "Method parameters cannot be null")
            : base(message)
        {
        }
    }
}