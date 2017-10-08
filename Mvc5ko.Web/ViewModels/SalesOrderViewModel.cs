using Mvc5ko.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5ko.Web.ViewModels
{
    public class SalesOrderViewModel : IObjectWithState
    {
        public int SalesOrderId { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }

        //public DateTime Date { get; set; }

        //public string Comment { get; set; }

        public string MessageToClient { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}