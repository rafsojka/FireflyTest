using System;

namespace Firefly.CodeTests.AuthManager.Exceptions
{
    public class InvalidUserNameException : Exception
    {
        public InvalidUserNameException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
