namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsStartedToAuction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "IsStarted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "IsStarted");
        }
    }
}
