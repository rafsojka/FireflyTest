namespace Firefly.CodeTests.AuthManager
{
    /// <summary>
    /// Implement this interface to provide custom validation.
    /// </summary>
    /// <remarks>
    /// For sample implementation see UserNameIsValidEmailAddressValidation
    /// </remarks>
    public interface IValidation<T> where T : class
    {
        /// <summary>
        /// Returns true when valid.
        /// </summary>
        bool IsValid(T user);

        /// <summary>
        /// The message when object is not valid.
        /// </summary>
        string Message { get; }
    }
}
