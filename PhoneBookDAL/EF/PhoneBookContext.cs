using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.EF
{
	public class PhoneBookContext : DbContext
	{
		static readonly DatabaseLogger Logger =
			new DatabaseLogger("D:\\db.log", true);

		public PhoneBookContext()
			: base("name=AzurePhoneBookConnection")
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

		public override int SaveChanges()
		{
			foreach (var entity in
				this.ChangeTracker.Entries()
				.Where(en => en.State == EntityState.Added))
			{
				DateTime now = DateTime.UtcNow;
				entity.Property("CreatedAt").CurrentValue = now;
				entity.Property("UpdatedAt").CurrentValue = now;
			}

			foreach (var entity in
				this.ChangeTracker.Entries()
				.Where(en => en.State == EntityState.Modified))
			{
				entity.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
			}

			return base.SaveChanges();
		}

		protected override void Dispose(bool disposing)
		{
			DbInterception.Remove(Logger);
			Logger.StopLogging();
			base.Dispose(disposing);
		}
	}
}
