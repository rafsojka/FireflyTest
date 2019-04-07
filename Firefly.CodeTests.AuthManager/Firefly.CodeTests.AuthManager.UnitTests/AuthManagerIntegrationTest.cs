using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Firefly.CodeTests.AuthManager.Exceptions;
using Firefly.CodeTests.AuthManager.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Firefly.CodeTests.AuthManager.UnitTests
{
    [TestClass]
    public class AuthManagerIntegrationTest
    {
        private const string _connectionString = @"Server=(localdb)\MSSQLLocalDB;DataBase=AuthManager;Integrated Security=SSPI";
        private readonly UserAuthManager _sutNoValidation = new UserAuthManager(new SqlUserRepository(_connectionString), null);

        [TestInitialize]
        public void TestInitialize()
        {
            // DB cleanup
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("TRUNCATE TABLE Users", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void CreateNewUser_UserNameValidationFails_1()
        {
            //	custom validation checks for UserName: should be a valid email address

            // arrange
            var sutWithEmailValidation = new UserAuthManager(new SqlUserRepository(_connectionString), new List<IValidation<User>> { new UserNameIsValidEmailAddressValidation() });
            try
            {
                // act
                sutWithEmailValidation.CreateUser(new User("notAValidEmail", "somePwd"));
                Assert.Fail("Expected exception on the previous line.");
            }
            catch (Exception ex)
            {
                // assert
                Assert.IsInstanceOfType(ex, typeof(ValidationException));
                Assert.AreEqual(new UserNameIsValidEmailAddressValidation().Message, ex.Message);
            }
        }

        [TestMethod]
        public void CreateNewUser_UserNameValidationFails_2()
        {
            //	custom validation checks for UserName: should be a valid active directory account (domain\username)

            // arrange
            var sutWithADValidation = new UserAuthManager(new SqlUserRepository(_connectionString), new List<IValidation<User>> { new UserNameIsValidActiveDirectoryAccountValidation() });
            try
            {
                // act
                sutWithADValidation.CreateUser(new User("notAValidADAccount", "somePwd"));
                Assert.Fail("Expected exception on the previous line.");
            }
            catch (Exception ex)
            {
                // assert
                Assert.IsInstanceOfType(ex, typeof(ValidationException));
                Assert.AreEqual(new UserNameIsValidActiveDirectoryAccountValidation().Message, ex.Message);
            }
        }

        [TestMethod]
        public void CreateNewUser_Succeeds()
        {
            try
            {
                // arrange + act
                _sutNoValidation.CreateUser(new User("testUserName", "testPwd"));
            }
            catch (Exception ex)
            {
                // assert
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserNameAlreadyExistsException))]
        public void CreateNewUser_UserNameAlreadyExists()
        {
            // arrange
            var userName = "testUsername";
            InsertUser(new User(userName, "testPwd"));

            // act + assert
            _sutNoValidation.CreateUser(new User(userName, "someOtherPwd"));
        }

        [TestMethod]
        public void GetUser_Succeeds()
        {
            // arrange
            var userName = "testUsername";
            var password = "testPassword";
            InsertUser(new User(userName, password));

            // act
            var returnedUser = _sutNoValidation.GetUser(userName);

            // assert
            Assert.AreEqual(userName, returnedUser.UserName);
            Assert.AreEqual(password, returnedUser.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserNameException))]
        public void GetUser_Fails()
        {
            // arrange + act + assert
            _sutNoValidation.GetUser("someNonExistentUserName");
        }

        [TestMethod]
        public void AuthenticaUserCredentialsOk_Succeeds()
        {
            // arrange
            var userName = "testUsername";
            var password = "testPassword";
            InsertUser(new User(userName, password));

            try
            {
                // act
                _sutNoValidation.AuthenticateUser(new User(userName, password));
            }
            catch (Exception ex)
            {
                // assert
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void AuthenticateUser_WrongPassword()
        {
            // arrange
            var userName = "testUsername";
            var password = "testPassword";
            InsertUser(new User(userName, password));

            // act + assert
            _sutNoValidation.AuthenticateUser(new User(userName, "otherPassword"));
        }

        private void InsertUser(User user)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand($"INSERT INTO Users (UserName, Password) VALUES('{user.UserName}', '{user.Password}')", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
