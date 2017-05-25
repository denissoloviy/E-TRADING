namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedImageEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "Extention", c => c.String(nullable: false));
            DropColumn("dbo.Images", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImagePath", c => c.String(nullable: false));
            DropColumn("dbo.Images", "Extention");
            DropColumn("dbo.Images", "Index");
        }
    }
}
