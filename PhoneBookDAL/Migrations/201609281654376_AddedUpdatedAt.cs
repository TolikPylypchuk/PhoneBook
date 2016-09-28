namespace PhoneBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUpdatedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserPhones", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.CompanyPhones", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reviews", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "UpdatedAt");
            DropColumn("dbo.CompanyPhones", "UpdatedAt");
            DropColumn("dbo.UserPhones", "UpdatedAt");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Companies", "UpdatedAt");
            DropColumn("dbo.Categories", "UpdatedAt");
            DropColumn("dbo.Addresses", "UpdatedAt");
        }
    }
}
