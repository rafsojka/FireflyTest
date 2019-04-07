using System;
using System.Text.RegularExpressions;

namespace Firefly.CodeTests.AuthManager
{
    public class UserNameIsValidActiveDirectoryAccountValidation : IValidation<User>
    {
        // based on https://stackoverflow.com/a/33079236/9205085
        public bool IsValid(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                return false;

            try
            {
                return Regex.IsMatch(user.UserName,
                    @"^[a-zA-Z][a-zA-Z0-9‌​\-\.]{0,61}[a-zA-Z]\\[a-zA-Z0-9‌​\-\.]*$",
                    RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public string Message
        {
            get
            {
                return "UserName is not a valid active directory account.";
            }
        }
    }
}
