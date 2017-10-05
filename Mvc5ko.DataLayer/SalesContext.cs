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

    public class SalesContext : DbContext
    {
        public DbSet<SalesOrder> SalesOrders { get; set; }

        //public SalesContext() : base("DefaultConnection")
        //{
            
        //}
    }
}
