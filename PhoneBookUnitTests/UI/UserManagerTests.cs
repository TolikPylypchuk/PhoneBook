using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBook.Services;
using PhoneBook.DAL.Models;

namespace PhoneBook.UnitTests.UI
{
    [TestClass]
    public class UserManagerTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void NoExistingUserTest()
        {
            UserManager.SetPassword(null, "password");
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void NullPasswordTest()
        {
            User user = new User();
            UserManager.SetPassword(user, null);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EmptyPasswordTest()
        {
            User user = new User();
            UserManager.SetPassword(user, String.Empty);
        }

        [TestMethod]
        public void SetPasswordTest()
        {
            User testUser1 = new User();
            UserManager.SetPassword(testUser1, "password");
            User testUser2 = new User();
            UserManager.SetPassword(testUser2, "password");
            Assert.AreEqual(testUser2.PasswordHash, testUser1.PasswordHash);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void SignInNullEmailTest()
        {
            UserManager.SignIn(null, String.Empty, new App(), null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void SignInNullPasswordTest()
        {
            UserManager.SignIn(String.Empty, null, new App(), null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void SignInNullAppTest()
        {
            UserManager.SignIn(String.Empty, String.Empty, null, null);
        }
    }
}
