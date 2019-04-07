namespace Firefly.CodeTests.AuthManager.Validation
{
    interface IValidator<T> where T : class
    {
        void Validate(T context);
    }
}
