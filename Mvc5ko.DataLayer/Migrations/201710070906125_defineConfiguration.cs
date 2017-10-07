namespace Mvc5ko.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defineConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesOrders", "CustomerName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.SalesOrders", "PONumber", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesOrders", "PONumber", c => c.String());
            AlterColumn("dbo.SalesOrders", "CustomerName", c => c.String());
        }
    }
}
