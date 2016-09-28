namespace PhoneBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUpdatedAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "UpdatedAt");
            DropColumn("dbo.Categories", "UpdatedAt");
            DropColumn("dbo.Companies", "UpdatedAt");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.UserPhones", "UpdatedAt");
            DropColumn("dbo.CompanyPhones", "UpdatedAt");
            DropColumn("dbo.Reviews", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.CompanyPhones", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.UserPhones", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Users", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Companies", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Categories", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Addresses", "UpdatedAt", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
