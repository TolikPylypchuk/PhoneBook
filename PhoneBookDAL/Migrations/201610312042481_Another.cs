using System.Data.Entity.Migrations;

namespace PhoneBook.DAL.Migrations
{
	public partial class Another : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Users", "Password", c => c.String(maxLength: 50));
			DropColumn("dbo.Users", "PasswordHash");
		}
        
		public override void Down()
		{
			AddColumn("dbo.Users", "PasswordHash", c => c.String(maxLength: 128));
			DropColumn("dbo.Users", "Password");
		}
	}
}
