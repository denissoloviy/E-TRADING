namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedForRegis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sellers", "Passport", c => c.String());
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "MiddleName");
            DropColumn("dbo.Sellers", "Passport");
        }
    }
}
