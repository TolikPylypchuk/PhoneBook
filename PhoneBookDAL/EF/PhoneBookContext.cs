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

		public DbSet<User> Users { get; set; }

		public DbSet<Company> Companies { get; set; }

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<UserPhone> UserPhones { get; set; }

		public DbSet<CompanyPhone> CompanyPhones { get; set; }

		protected override void Dispose(bool disposing)
		{
			DbInterception.Remove(Logger);
			Logger.StopLogging();
			base.Dispose(disposing);
		}
	}
}
