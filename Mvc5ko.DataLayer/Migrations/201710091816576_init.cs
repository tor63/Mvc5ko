namespace Mvc5ko.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesOrderItems",
                c => new
                    {
                        SalesOrderItemId = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(nullable: false, maxLength: 15),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesOrderItemId)
                .ForeignKey("dbo.SalesOrders", t => t.SalesOrderId, cascadeDelete: true)
                .Index(t => t.SalesOrderId);
            
            CreateTable(
                "dbo.SalesOrders",
                c => new
                    {
                        SalesOrderId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 30),
                        PONumber = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.SalesOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesOrderItems", "SalesOrderId", "dbo.SalesOrders");
            DropIndex("dbo.SalesOrderItems", new[] { "SalesOrderId" });
            DropTable("dbo.SalesOrders");
            DropTable("dbo.SalesOrderItems");
        }
    }
}
