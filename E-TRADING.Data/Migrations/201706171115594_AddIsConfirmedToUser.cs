namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsConfirmedToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "ErrorText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ErrorText");
            DropColumn("dbo.AspNetUsers", "IsConfirmed");
        }
    }
}
