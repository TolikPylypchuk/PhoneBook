using System.Data.Entity.Migrations;

namespace PhoneBook.DAL.Migrations
{
    public partial class PasswordHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Number", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String(maxLength: 50));
            AlterColumn("dbo.Addresses", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "PasswordHash");
        }
    }
}
