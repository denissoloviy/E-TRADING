namespace E_TRADING.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceNumberToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "InvoiceNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "InvoiceNumber");
        }
    }
}
