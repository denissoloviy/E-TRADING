namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsConfirmedToSeller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sellers", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sellers", "ErrorText", c => c.String());
            DropColumn("dbo.AspNetUsers", "IsConfirmed");
            DropColumn("dbo.AspNetUsers", "ErrorText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ErrorText", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsConfirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Sellers", "ErrorText");
            DropColumn("dbo.Sellers", "IsConfirmed");
        }
    }
}
