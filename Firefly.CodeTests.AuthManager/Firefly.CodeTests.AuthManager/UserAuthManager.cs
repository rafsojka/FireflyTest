using Firefly.CodeTests.AuthManager.Validation;
using System.Collections.Generic;

namespace Firefly.CodeTests.AuthManager
{
    /// <summary>
    /// The UserAuthManager class.
    /// Contains methods creating, getting and authenticating a User.
    /// </summary>
    /// <remarks>
    /// This class is specialised to opperate on User type.
    /// </remarks>
    public class UserAuthManager : IUserAuthManager
    {
        private readonly IUserRepository userRepository;
        private readonly IValidator<User> validator;

        /// <summary>
        /// UserAuthManager constructor.
        /// </summary>
        /// <param name="userRepository">User repository instance.</param>
        /// <param name="validations">Collection of validations to be performed on User during Create operation.</param>
        public UserAuthManager(IUserRepository userRepository, IEnumerable<IValidation<User>> validations)
        {
            this.userRepository = userRepository;
            this.validator = new UserValidator(validations);
        }

        /// <summary>
        /// CreateUser operation.
        /// </summary>
        /// <remarks>
        /// Operation successful when no exception thrown.
        /// </remarks>
        /// <param name="user">User instance.</param>
        /// <exception cref="Exceptions.ValidationException">
        /// Thrown when passed User fails at least one validation from the list specified in constructor.</exception>
        /// <exception cref="Exceptions.UserNameAlreadyExistsException">
        /// Thrown when there is already a record in database with UserName equal to passed User.UserName.</exception>
        /// <exception cref="Exceptions.UserRepositoryException">
        /// Thrown when database error occured. See InnerException for details.</exception>
        public void CreateUser(User user)
        {
            validator.Validate(user);

            userRepository.CreateUser(user);
        }

        /// <summary>
        /// GetUser operation.
        /// </summary>
        /// /// <returns>
        /// The User found with matching UserName.
        /// </returns>
        /// <param name="userName">UserName of the user to get.</param>
        /// <exception cref="Exceptions.InvalidUserNameException">
        /// Thrown when passed UserName cannot be found in database.</exception>
        /// <exception cref="Exceptions.UserRepositoryException">
        /// Thrown when database error occured. See InnerException for details.</exception>
        public User GetUser(string userName)
        {
            return userRepository.GetUser(userName);
        }

        /// <summary>
        /// AuthenticateUser operation.
        /// </summary>
        /// /// <remarks>
        /// Operation successful when no exception thrown.
        /// </remarks>
        /// <param name="user">User instance.</param>
        /// <exception cref="Exceptions.InvalidUserNameException">
        /// Thrown when passed UserName cannot be found in database.</exception>
        /// <exception cref="Exceptions.InvalidPasswordException">
        /// Thrown when passed Password does not match the password stored in database for the given User.UserName.</exception>
        /// <exception cref="Exceptions.UserRepositoryException">
        /// Thrown when database error occured. See InnerException for details.</exception>
        public void AuthenticateUser(User user)
        {
            userRepository.AuthenticateUser(user);
        }
    }
}
