namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStartPriceToAuctionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "StartPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "StartPrice");
        }
    }
}
