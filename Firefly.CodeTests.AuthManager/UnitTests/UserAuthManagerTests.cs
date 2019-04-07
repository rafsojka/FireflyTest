using Firefly.CodeTests.AuthManager;
using Firefly.CodeTests.AuthManager.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class UserAuthManagerTests
    {
        [TestMethod]
        public void GetUser_ThrowsInvalidUserNameException_WhenRepositoryThrowsInvalidUserNameException()
        {
            // arrange
            var exMessage = "Invalid username";
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock.Setup(m => m.GetUser(It.IsAny<string>())).Throws(new InvalidUserNameException(exMessage, null));

            var sut = new UserAuthManager(repositoryMock.Object, null);

            // act
            var ex = Assert.ThrowsException<InvalidUserNameException>(() => sut.GetUser("someUserName"));

            // assert
            Assert.AreEqual(exMessage, ex.Message);
        }

        // TODO: further unit test for UserAuthManager
    }
}
