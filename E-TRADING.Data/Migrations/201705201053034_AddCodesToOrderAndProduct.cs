namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodesToOrderAndProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderNumber", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Products", "ProductCode", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductCode");
            DropColumn("dbo.Orders", "OrderNumber");
        }
    }
}
