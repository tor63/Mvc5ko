namespace Mvc5ko.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteCols : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SalesOrders", "Date");
            DropColumn("dbo.SalesOrders", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrders", "Comment", c => c.String());
            AddColumn("dbo.SalesOrders", "Date", c => c.DateTime(nullable: false));
        }
    }
}
