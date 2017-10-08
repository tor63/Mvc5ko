using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc5ko.Model
{
    public class SalesOrder : IObjectWithState
    {
        //sdfsdf
        public int SalesOrderId { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }
        public ObjectState ObjectState { get; set; }


        //public DateTime Date { get; set; }

        //public string Comment { get; set; }

    }
}
