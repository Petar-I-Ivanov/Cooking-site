namespace CookingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCategoryImgPath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "ImagePath", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "ImagePath", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
