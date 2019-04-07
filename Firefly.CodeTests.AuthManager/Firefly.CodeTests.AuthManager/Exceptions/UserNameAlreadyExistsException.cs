using System;

namespace Firefly.CodeTests.AuthManager.Exceptions
{
    public class UserNameAlreadyExistsException : Exception
    {
        public UserNameAlreadyExistsException(string message, params object[] args)
            : base(String.Format(message, args))
        {
        }
    }
}
