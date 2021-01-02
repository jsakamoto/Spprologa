using System;

namespace Spprologa
{
    public class PrologException : Exception
    {
        public PrologException(string? message) : base(message)
        {
        }

        public PrologException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
