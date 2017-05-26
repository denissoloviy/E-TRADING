namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsDeletedChangedImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sellers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Images", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "Extention", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "ShippingAddress", c => c.String(nullable: false));
            DropColumn("dbo.Images", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImagePath", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "ShippingAddress", c => c.String());
            DropColumn("dbo.Images", "Extention");
            DropColumn("dbo.Images", "Index");
            DropColumn("dbo.Sellers", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Buyers", "IsDeleted");
        }
    }
}
