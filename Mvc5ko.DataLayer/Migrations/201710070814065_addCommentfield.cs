namespace Mvc5ko.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCommentfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrders", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesOrders", "Comment");
        }
    }
}
