using System;
using System.Collections.Generic;

namespace Firefly.CodeTests.AuthManager.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(IEnumerable<string> messages)
            : base(string.Join("\n",messages))
        {
        }
    }
}
