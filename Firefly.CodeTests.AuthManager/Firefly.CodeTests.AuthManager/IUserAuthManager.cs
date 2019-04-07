namespace Firefly.CodeTests.AuthManager
{
    public interface IUserAuthManager
    {
        void AuthenticateUser(User user);
        void CreateUser(User user);
        User GetUser(string userName);
    }
}