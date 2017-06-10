namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIsArchivedFromAuction : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Auctions", "IsArchived");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "IsArchived", c => c.Boolean(nullable: false));
        }
    }
}
