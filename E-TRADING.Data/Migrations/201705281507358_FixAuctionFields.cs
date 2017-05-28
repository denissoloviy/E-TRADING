namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixAuctionFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auctions", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.Auctions", new[] { "BuyerId" });
            AddColumn("dbo.Auctions", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Auctions", "BuyerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Auctions", "BuyerId");
            AddForeignKey("dbo.Auctions", "BuyerId", "dbo.Buyers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.Auctions", new[] { "BuyerId" });
            AlterColumn("dbo.Auctions", "BuyerId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Auctions", "IsDeleted");
            CreateIndex("dbo.Auctions", "BuyerId");
            AddForeignKey("dbo.Auctions", "BuyerId", "dbo.Buyers", "Id", cascadeDelete: true);
        }
    }
}
