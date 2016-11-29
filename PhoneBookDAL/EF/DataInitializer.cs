using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.EF
{
	public class DataInitializer : DropCreateDatabaseAlways<PhoneBookContext>
	{
		protected override void Seed(PhoneBookContext context)
		{
			Address address1 = new Address
			{
				Country = "Ukraine",
				City = "Lviv",
				Street = "Horodotska st.",
				Number = "311",
				Apartment = 34,
				PostalCode = 79000
			};

			Address address2 = new Address
			{
				Country = "Ukraine",
				City = "Lviv",
				Street = "Kyivska st.",
				Number = "21",
				Apartment = 4,
				PostalCode = 79013
			};

			Address address3 = new Address
			{
				Country = "Ukraine",
				City = "Lviv",
				Street = "Sadova st.",
				Number = "2a",
				PostalCode = 79017
			};

			string password = "111";
			var data = Encoding.UTF8.GetBytes(password);
			string hash = null;

			using (var sha256 = SHA256.Create())
			{
				var hashData = sha256.ComputeHash(data);
				hash = Encoding.UTF8.GetString(hashData);
			}

			User natalia = new User
			{
				FirstName = "Nataliia",
				MiddleName = "Andriivna",
				LastName = "Slobodianiuk",
				Address = address1,
				Email = "slo0nata@gmail.com",
				PasswordHash = hash,
				IsVisible = true
			};

			User tolik = new User
			{
				FirstName = "Anatoliy",
				MiddleName = "Ihorovych",
				LastName = "Pylypchuk",
				Email = "pylypchuk.tolik@gmail.com",
				Address = address2,
				PasswordHash = hash,
				IsVisible = true
			};

			UserPhone phone1 = new UserPhone
			{
				User = natalia,
				Number = "+380975749621",
			};

			UserPhone phone2 = new UserPhone
			{
				User = tolik,
				Number = "+380500232686"
			};

			UserPhone phone3 = new UserPhone
			{
				User = tolik,
				Number = "+380322378667"
			};

			Category it = new Category { Name = "IT" };

			Company softServe = new Company
			{
				Name = "SoftServe",
				Category = it,
				Address = address3,
				Email = "hr@softserveinc.com",
				Description = "SoftServe is the largest global " +
					"IT company with Ukrainian roots specializing " +
					"in software development and consultancy services.",
				CreatedBy = tolik
			};

			CompanyPhone phone4 = new CompanyPhone
			{
				Number = "+380322409999",
				Company = softServe
			};

			CompanyPhone phone5 = new CompanyPhone
			{
				Number = "+380322409080",
				Company = softServe
			};

			context.Addresses.Add(address1);
			context.Addresses.Add(address2);
			context.Addresses.Add(address3);

			context.Users.Add(natalia);
			context.Users.Add(tolik);
			
			context.UserPhones.Add(phone1);
			context.UserPhones.Add(phone2);
			context.UserPhones.Add(phone3);

			context.Categories.Add(it);

			context.Companies.Add(softServe);

			context.CompanyPhones.Add(phone4);
			context.CompanyPhones.Add(phone5);

			context.SaveChanges();
		}
	}
}
