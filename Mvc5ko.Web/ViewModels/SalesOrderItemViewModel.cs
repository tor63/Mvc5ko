using Mvc5ko.Model;

namespace Mvc5ko.Web.ViewModels
{
    public class SalesOrderItemViewModel : IObjectWithState
    {
        public int SalesOrderItemId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int SalesOrderId { get; set; }

        public ObjectState ObjectState { get; set; }
    }
}