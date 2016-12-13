using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using PhoneBook.DAL.Models;
using PhoneBook.DAL.Repositories;

namespace PhoneBook.Services
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

			var hashData = GetHashPassword(password);

			user.PasswordHash = hashData;
		}

		public static SignUpResult SignUp(User user, IRepository<User> users)
		{
			if (user == null)
			{
				throw new ArgumentNullException(
					nameof(user),
					"The user cannot be null.");
			}

			try
			{
				var dbUser = users.GetAll().FirstOrDefault(
					u => u.Email == user.Email);

				if (dbUser != null)
				{
					return SignUpResult.EmailAlreadyOccupied;
				}

				users.Add(user);
			} catch (DbEntityValidationException exp)
			{
				return exp.EntityValidationErrors.Any(
					error => error.ValidationErrors.Any(
						e => e.ErrorMessage.Contains("required")))
					? SignUpResult.NotAllPropertiesSet
					: SignUpResult.ValidationError;
			}

			return SignUpResult.Success;
		}

		public static User SignIn(
			string email,
			string password,
			App app,
			IRepository<User> users)
		{
			if (email == null)
			{
				throw new ArgumentNullException(
					nameof(email),
					"The email cannot be null.");
			}

			if (password == null)
			{
				throw new ArgumentNullException(
					nameof(password),
					"The email cannot be null.");
			}

			if (app == null)
			{
				throw new ArgumentNullException(
					nameof(app),
					"The app cannot be null.");
			}

			User user = null;

			string hashPassword = GetHashPassword(password);

			try
			{
				user = users.GetAll().FirstOrDefault(u =>
					((u.PasswordHash == hashPassword) && (u.Email == email)));
			}
			catch (DbUnexpectedValidationException e)
			{
				throw new Exception("Error in DB work occured.", e);
			}

			app.CurrentUser = user;

			return user;
		}

		private static string GetHashPassword(string password)
		{
			string hashPassword = String.Empty;

			var data = Encoding.UTF8.GetBytes(password);

			using (var sha256 = SHA256.Create())
			{
				var hashData = sha256.ComputeHash(data);
				hashPassword = Encoding.UTF8.GetString(hashData);
			}

			return hashPassword;
		}
	}
}
