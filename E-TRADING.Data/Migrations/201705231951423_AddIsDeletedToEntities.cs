namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sellers", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sellers", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Buyers", "IsDeleted");
        }
    }
}
