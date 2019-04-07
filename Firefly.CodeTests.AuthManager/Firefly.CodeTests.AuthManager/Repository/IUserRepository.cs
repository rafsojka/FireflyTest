namespace Firefly.CodeTests.AuthManager
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUser(string userName);
        void AuthenticateUser(User user);
    }
}
