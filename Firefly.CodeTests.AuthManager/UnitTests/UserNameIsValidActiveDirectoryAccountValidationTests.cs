using Firefly.CodeTests.AuthManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UserNameIsValidActiveDirectoryAccountValidationTests
    {
        [DataTestMethod]
        [DataRow(@"domain\username")]
        [DataRow(@"Domain10S\U8erNamE")]
        public void IsValid_ReturnsTrue_WhenUserNameIsValidActiveDirectoryAccount(string username)
        {
            // arrange
            var user = new User(username, string.Empty);
            var sut = new UserNameIsValidActiveDirectoryAccountValidation();

            // act
            var validationResult = sut.IsValid(user);

            // assert
            Assert.IsTrue(validationResult);
        }

        [DataTestMethod]
        [DataRow(@"domainNoBaskslashUsername")]
        [DataRow(@"domain/username")]
        [DataRow(@"double\\backslash")]
        public void IsValid_ReturnsFalse_WhenUserNameIsNotValidActiveDirectoryAccount(string username)
        {
            // arrange
            var user = new User(username, string.Empty);
            var sut = new UserNameIsValidActiveDirectoryAccountValidation();

            // act
            var validationResult = sut.IsValid(user);

            // assert
            Assert.IsFalse(validationResult);
        }
    }
}
