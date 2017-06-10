namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedFieldsInAuction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Auctions", "IsFinished", c => c.Boolean(nullable: false));
            DropColumn("dbo.Auctions", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Auctions", "IsFinished");
            DropColumn("dbo.Auctions", "IsArchived");
        }
    }
}
