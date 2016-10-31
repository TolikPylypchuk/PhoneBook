using System.Data.Entity.Migrations;

using PhoneBook.DAL.EF;

namespace PhoneBook.DAL.Migrations
{
	internal sealed class Configuration
		: DbMigrationsConfiguration<PhoneBookContext>
	{
		public Configuration()
		{
			this.AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(PhoneBookContext context)
		{
		}
	}
}
