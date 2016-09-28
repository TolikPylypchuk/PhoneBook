using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.EF
{
	public class PhoneBookContext : DbContext
	{
		static readonly DatabaseLogger Logger =
			new DatabaseLogger("db.log", true);

		public PhoneBookContext()
			: base("name=PhoneBookConnection")
		{
			Logger.StartLogging();
			DbInterception.Add(Logger);
		}

		DbSet<User> Users { get; set; }

		DbSet<Company> Companies { get; set; }

		DbSet<Address> Addresses { get; set; }

		DbSet<Category> Categories { get; set; }

		DbSet<Review> Reviews { get; set; }

		DbSet<UserPhone> UserPhones { get; set; }

		DbSet<CompanyPhone> CompanyPhones { get; set; }

		protected override void Dispose(bool disposing)
		{
			DbInterception.Remove(Logger);
			Logger.StopLogging();
			base.Dispose(disposing);
		}
	}
}
