using System.Data.Entity.Migrations;

namespace PhoneBook.DAL.Migrations
{
	public partial class Initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Addresses",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Country = c.String(nullable: false, maxLength: 20),
						City = c.String(nullable: false, maxLength: 50),
						Street = c.String(nullable: false, maxLength: 50),
						Number = c.Int(nullable: false),
						Apartment = c.Int(nullable: false),
						PostalCode = c.Int(nullable: false),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id);
            
			CreateTable(
				"dbo.Categories",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 50),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id);
            
			CreateTable(
				"dbo.Companies",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 20),
						Description = c.String(nullable: false, maxLength: 300),
						Email = c.String(maxLength: 20),
						UserId = c.Int(nullable: false),
						AddressId = c.Int(nullable: false),
						CategoryId = c.Int(nullable: false),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
				.ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
				.ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
				.Index(t => t.UserId)
				.Index(t => t.AddressId)
				.Index(t => t.CategoryId);
            
			CreateTable(
				"dbo.Users",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Email = c.String(nullable: false, maxLength: 50),
						Password = c.String(maxLength: 50),
						FirstName = c.String(nullable: false, maxLength: 15),
						MiddleName = c.String(maxLength: 15),
						LastName = c.String(nullable: false, maxLength: 15),
						AddressId = c.Int(nullable: false),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
				.Index(t => t.AddressId);
            
			CreateTable(
				"dbo.UserPhones",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Number = c.String(nullable: false, maxLength: 15),
						UserId = c.Int(nullable: false),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);
            
			CreateTable(
				"dbo.CompanyPhones",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Number = c.String(nullable: false, maxLength: 15),
						CompanyId = c.Int(nullable: false),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
				.Index(t => t.CompanyId);
            
			CreateTable(
				"dbo.Reviews",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Description = c.String(nullable: false, maxLength: 300),
						Rating = c.Int(nullable: false),
						UserId = c.Int(nullable: false),
						CompanyId = c.Int(nullable: false),
						CreatedAt = c.DateTime(nullable: false),
						UpdatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
				.ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId)
				.Index(t => t.CompanyId);
            
		}
        
		public override void Down()
		{
			DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
			DropForeignKey("dbo.Reviews", "CompanyId", "dbo.Companies");
			DropForeignKey("dbo.CompanyPhones", "CompanyId", "dbo.Companies");
			DropForeignKey("dbo.Companies", "UserId", "dbo.Users");
			DropForeignKey("dbo.UserPhones", "UserId", "dbo.Users");
			DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
			DropForeignKey("dbo.Companies", "CategoryId", "dbo.Categories");
			DropForeignKey("dbo.Companies", "AddressId", "dbo.Addresses");
			DropIndex("dbo.Reviews", new[] { "CompanyId" });
			DropIndex("dbo.Reviews", new[] { "UserId" });
			DropIndex("dbo.CompanyPhones", new[] { "CompanyId" });
			DropIndex("dbo.UserPhones", new[] { "UserId" });
			DropIndex("dbo.Users", new[] { "AddressId" });
			DropIndex("dbo.Companies", new[] { "CategoryId" });
			DropIndex("dbo.Companies", new[] { "AddressId" });
			DropIndex("dbo.Companies", new[] { "UserId" });
			DropTable("dbo.Reviews");
			DropTable("dbo.CompanyPhones");
			DropTable("dbo.UserPhones");
			DropTable("dbo.Users");
			DropTable("dbo.Companies");
			DropTable("dbo.Categories");
			DropTable("dbo.Addresses");
		}
	}
}
