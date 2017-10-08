using Mvc5ko.Model;
using System.Data.Entity.ModelConfiguration;

namespace Mvc5ko.DataLayer
{
    public class SalesOrderConfiguration : EntityTypeConfiguration<SalesOrder>
    {
        public SalesOrderConfiguration()
        {
            Property(so => so.CustomerName).HasMaxLength(30).IsRequired();
            Property(so => so.PONumber).HasMaxLength(10).IsOptional();
            Ignore(so => so.ObjectState);
        }
    }
}
