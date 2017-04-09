namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuctionAndShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        MinStep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastBid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyerId = c.String(nullable: false, maxLength: 128),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.BuyerId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Int(nullable: false),
                        BuyerId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.BuyerId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "Id", "dbo.Products");
            DropForeignKey("dbo.Auctions", "BuyerId", "dbo.Buyers");
            DropForeignKey("dbo.ShoppingCarts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ShoppingCarts", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.ShoppingCarts", new[] { "ProductId" });
            DropIndex("dbo.ShoppingCarts", new[] { "BuyerId" });
            DropIndex("dbo.Auctions", new[] { "BuyerId" });
            DropIndex("dbo.Auctions", new[] { "Id" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Auctions");
        }
    }
}
