using System;

namespace Firefly.CodeTests.AuthManager.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
