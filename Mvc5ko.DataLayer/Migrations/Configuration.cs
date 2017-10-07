namespace Mvc5ko.DataLayer.Migrations
{
    using Mvc5ko.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mvc5ko.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Mvc5ko.DataLayer.SalesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //context.SalesOrders.AddOrUpdate(
            //    so => so.CustomerName,
            //    new SalesOrder { CustomerName = "Adam", PONumber = "1234", Date = DateTime.Now },
            //    new SalesOrder { CustomerName = "Michal", Date = DateTime.Now },
            //    new SalesOrder { CustomerName = "David", PONumber = "PO 222", Date = DateTime.Now, Comment = "kommentar for David" }
            //    );
            context.SalesOrders.AddOrUpdate(
                         so => so.CustomerName,
                         new SalesOrder { CustomerName = "Adam", PONumber = "1234",  },
                         new SalesOrder { CustomerName = "Michal" },
                         new SalesOrder { CustomerName = "David", PONumber = "PO 222"}
                         );

        }
    }
}
