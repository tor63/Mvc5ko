using Mvc5ko.Model;
using System.Collections.Generic;

namespace Mvc5ko.Web.ViewModels
{
    //Server side view model
    public class SalesOrderViewModel : IObjectWithState
    {
        public SalesOrderViewModel()
        {
            SalesOrderItems = new List<SalesOrderItemViewModel>();
        }

        public int SalesOrderId { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }
        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; }

        public string MessageToClient { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}