using System;

namespace Shops.Tools
{
    public class ShopException : Exception
    {
        public ShopException(string message)
            : base(message)
        {
        }
    }
}