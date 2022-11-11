namespace CookingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Desription = c.String(nullable: false, maxLength: 500),
                        ImagePath = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CategoryId = c.Long(nullable: false),
                        Title = c.String(nullable: false, maxLength: 30),
                        Desription = c.String(nullable: false, maxLength: 500),
                        NeededProducts = c.Int(nullable: false),
                        PublishedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RecipeId = c.Long(nullable: false),
                        Content = c.String(nullable: false, maxLength: 200),
                        Author = c.String(maxLength: 50),
                        PublishedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.Recipe", "CategoryId", "dbo.Category");
            DropIndex("dbo.Comment", new[] { "RecipeId" });
            DropIndex("dbo.Recipe", new[] { "CategoryId" });
            DropTable("dbo.Comment");
            DropTable("dbo.Recipe");
            DropTable("dbo.Category");
        }
    }
}
