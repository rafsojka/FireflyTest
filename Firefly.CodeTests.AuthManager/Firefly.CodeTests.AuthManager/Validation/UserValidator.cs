using Firefly.CodeTests.AuthManager.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Firefly.CodeTests.AuthManager.Validation
{
    class UserValidator : IValidator<User>
    {
        private readonly IEnumerable<IValidation<User>> validations;

        public UserValidator(IEnumerable<IValidation<User>> validations)
        {
            this.validations = validations;
        }

        public void Validate(User user)
        {
            if (validations != null && validations.Any(v => !v.IsValid(user)))
            {
                throw new ValidationException(validations.Where(v => !v.IsValid(user)).Select(v => v.Message));
            }
        }
    }
}
