using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc5ko.Model;

namespace Mvc5ko.DataLayer
{
    //PM commands:
    //migrate : enable-migrations -EnableAutomaticMigrations
    //update-database -Verbose

    //OR
    //migrate : enable-migrations 
    //add-migration Initial
    //update-database -Verbose

    //View the datamodell:
    // - install the VS extension: Entity Framework 6 Power tools
    // - make this project as startup
    // - Rightclick the context-class and select EntityFramework

    public class SalesContext : DbContext
    {
        public DbSet<SalesOrder> SalesOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SalesOrderConfiguration());
        }

        //Constructor can be used if you want to specify database name!
        //For switching to full sql version see: https://dotnetpanda.wordpress.com/2016/05/30/entity-framework-jump-start-with-asp-net-mvc-and-sql-server/
        public SalesContext() : base("DefaultConnection")
        {

        }
    }
}
