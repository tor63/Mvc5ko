namespace Mvc5ko.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrders", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesOrders", "Date");
        }
    }
}
