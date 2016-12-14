using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PhoneBook.Services;
using PhoneBook.DAL.Models;

namespace PhoneBook.UnitTests.Services
{
    [TestClass]
    public class UserManagerTests
    {
		static App app = new App();

        [TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NoExistingUserTest()
        {
            UserManager.SetPassword(null, "password");
        }

        [TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullPasswordTest()
        {
            User user = new User();
            UserManager.SetPassword(user, null);
        }

        [TestMethod]
		[ExpectedException(typeof(ArgumentException))]
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

        [TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SignInNullEmailTest()
        {
            UserManager.SignIn(null, String.Empty, app, null);
        }

        [TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SignInNullPasswordTest()
        {
            UserManager.SignIn(String.Empty, null, app, null);
        }

        [TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SignInNullAppTest()
        {
            UserManager.SignIn(String.Empty, String.Empty, null, null);
        }
    }
}
