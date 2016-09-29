using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

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

			var objContext = (this as IObjectContextAdapter).ObjectContext;
			objContext.SavingChanges += AddTimeProperties;
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Company> Companies { get; set; }

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<UserPhone> UserPhones { get; set; }

		public DbSet<CompanyPhone> CompanyPhones { get; set; }

		private void AddTimeProperties(object sender, EventArgs e)
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
		}

		protected override void Dispose(bool disposing)
		{
			DbInterception.Remove(Logger);
			Logger.StopLogging();
			base.Dispose(disposing);
		}
	}
}
