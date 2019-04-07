using System;

namespace Firefly.CodeTests.AuthManager.Exceptions
{
    public class UserRepositoryException : Exception
    {
        public UserRepositoryException(string methodName, Exception innerException)
            : base($"Exception occured during execution of {methodName}", innerException)
        {

        }
    }
}
