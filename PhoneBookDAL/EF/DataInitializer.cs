using System;
using System.Data.Entity;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.EF
{
	public class DataInitializer : DropCreateDatabaseAlways<PhoneBookContext>
	{
		protected override void Seed(PhoneBookContext context)
		{
			Address address1 = new Address()
			{
				Country = "Ukraine",
				City = "Lviv",
				Street = "Horodotska st.",
				Number = 311,
				Apartment = 34,
				PostalCode = 79000
			};

			Address address2 = new Address()
			{
				Country = "Ukraine",
				City = "Lviv",
				Street = "Kyivska st.",
				Number = 21,
				Apartment = 4,
				PostalCode = 79013
			};

			User natalia = new User()
			{
				FirstName = "Nataliia",
				MiddleName = "Andriivna",
				LastName = "Slobodianiuk",
				Email = "slo0nata@gmail.com",
				Address = address1
			};

			User tolik = new User()
			{
				FirstName = "Anatoliy",
				MiddleName = "Ihorovych",
				LastName = "Pylypchuk",
				Email = "pylypchuk.tolik@gmail.com",
				Address = address2
			};

			UserPhone phone1 = new UserPhone()
			{
				User = natalia,
				Number = "+380975749621",
			};

			UserPhone phone2 = new UserPhone()
			{
				User = tolik,
				Number = "+380500232686"
			};
			
			context.Addresses.Add(address1);
			context.Addresses.Add(address2);

			context.Users.Add(natalia);
			context.Users.Add(tolik);
			
			context.UserPhones.Add(phone1);
			context.UserPhones.Add(phone2);

			context.SaveChanges();
		}
	}
}
