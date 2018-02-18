using System;

namespace BusinessLogic.Exceptions
{
    public class UriInsteadOfLocalPathException : Exception
    {
        public UriInsteadOfLocalPathException()
        {
        }

        public UriInsteadOfLocalPathException(string message)
            :base(message)
        {
        }

        public UriInsteadOfLocalPathException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}