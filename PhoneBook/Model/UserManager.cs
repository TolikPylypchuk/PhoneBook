using System;
using System.Security.Cryptography;
using System.Text;

using PhoneBook.DAL.Models;

namespace PhoneBook.Model
{
	public static class UserManager
	{
		public enum SignUpResult
		{
			Success,
			NotAllPropertiesSet,
			ValidationError,
			EmailAlreadyOccupied
		}

		public static void SetPassword(User user, string password)
		{
			if (user == null)
			{
				throw new ArgumentNullException(
					nameof(user),
					"The user cannot be null.");
			}

			if (password == null)
			{
				throw new ArgumentNullException(
					nameof(password),
					"The password cannot be null.");
			}

			if (password == String.Empty)
			{
				throw new ArgumentException(
					"The password cannot be empty.",
					nameof(password));
			}

			var data = Encoding.UTF8.GetBytes(password);

			using (var sha256 = SHA256.Create())
			{
				var hashData = sha256.ComputeHash(data);
				user.PasswordHash = Encoding.UTF8.GetString(hashData);
			}
		}

		public static SignUpResult SignUp(User user, App app)
		{
			if (user == null)
			{
				throw new ArgumentNullException(
					nameof(user),
					"The user cannot be null.");
			}

			if (app == null)
			{
				throw new ArgumentNullException(
					nameof(app),
					"The app cannot be null.");
			}

			return SignUpResult.NotAllPropertiesSet;
		}
	}
}
