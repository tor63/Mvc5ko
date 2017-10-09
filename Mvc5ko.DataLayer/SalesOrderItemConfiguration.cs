using Mvc5ko.Model;
using System.Data.Entity.ModelConfiguration;

namespace Mvc5ko.DataLayer
{
    public class SalesOrderItemConfiguration : EntityTypeConfiguration<SalesOrderItem>
    {
        public SalesOrderItemConfiguration()
        {
            Property(soi => soi.ProductCode).HasMaxLength(15).IsRequired();
            Property(soi => soi.Quantity).IsRequired();
            Property(soi => soi.UnitPrice).IsRequired();
            Ignore(soi => soi.ObjectState);
        }
    }
}
