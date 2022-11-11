namespace CookingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRecipeDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipe", "Desription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipe", "Desription", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
