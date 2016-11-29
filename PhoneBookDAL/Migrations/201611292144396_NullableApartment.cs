using System.Data.Entity.Migrations;

namespace PhoneBook.DAL.Migrations
{
    public partial class NullableApartment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Apartment", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Apartment", c => c.Int(nullable: false));
        }
    }
}
