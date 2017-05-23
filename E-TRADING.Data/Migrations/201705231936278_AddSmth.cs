namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ShippingAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ShippingAddress", c => c.String());
        }
    }
}
