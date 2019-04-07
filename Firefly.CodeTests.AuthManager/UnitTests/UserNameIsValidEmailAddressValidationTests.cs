using Firefly.CodeTests.AuthManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UserNameIsValidEmailAddressValidationTests
    {
        [DataTestMethod]
        [DataRow("david.jones@proseware.com")]
        [DataRow("d.j@server1.proseware.com")]
        [DataRow("jones@ms1.proseware.com")]
        [DataRow("j@proseware.com9")]
        [DataRow("js#internal@proseware.com")]
        [DataRow("j_9@[129.126.118.1]")]
        [DataRow("js@proseware.com9")]
        [DataRow("j.s@server1.proseware.com")]
        public void IsValid_ReturnsTrue_WhenUserNameIsValidEmailAddress(string username)
        {
            // arrange
            var user = new User(username, string.Empty);
            var sut = new UserNameIsValidEmailAddressValidation();

            // act
            var validationResult = sut.IsValid(user);

            // assert
            Assert.IsTrue(validationResult);
        }

        [DataTestMethod]
        [DataRow("j.@server1.proseware.com")]
        [DataRow("j..s@proseware.com")]
        [DataRow("js*@proseware.com")]
        [DataRow("js@proseware..com")]
        public void IsValid_ReturnsFalse_WhenUserNameIsNotValidEmailAddress(string username)
        {
            // arrange
            var user = new User(username, string.Empty);
            var sut = new UserNameIsValidEmailAddressValidation();

            // act
            var validationResult = sut.IsValid(user);

            // assert
            Assert.IsFalse(validationResult);
        }
    }
}
