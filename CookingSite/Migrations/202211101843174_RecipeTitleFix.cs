namespace CookingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeTitleFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipe", "Title", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipe", "Title", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
