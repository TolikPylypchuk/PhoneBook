using System.Data.Entity.Migrations;

namespace PhoneBook.DAL.Migrations
{
    public partial class UserIsVisible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsVisible");
        }
    }
}
