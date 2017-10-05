namespace Mvc5ko.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesOrders",
                c => new
                    {
                        SalesOrderId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        PONumber = c.String(),
                    })
                .PrimaryKey(t => t.SalesOrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalesOrders");
        }
    }
}
