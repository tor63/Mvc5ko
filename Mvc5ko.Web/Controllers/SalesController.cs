using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc5ko.DataLayer;
using Mvc5ko.Model;
using Mvc5ko.Web.ViewModels;

namespace Mvc5ko.Web.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext _salesContext;// = new SalesContext();

        public SalesController()
        {
            _salesContext = new SalesContext();
        }

        public ActionResult Index()
        {
            return View(_salesContext.SalesOrders.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            //Added code:
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel
            {
                SalesOrderId = salesOrder.SalesOrderId,
                CustomerName = salesOrder.CustomerName,
                PONumber = salesOrder.PONumber,
                MessageToClient = "I originated from the viewmodel, rather than the model."
            };

            return View(salesOrderViewModel);
        }

        public ActionResult Create()
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(salesOrderViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel
            {
                SalesOrderId = salesOrder.SalesOrderId,
                CustomerName = salesOrder.CustomerName,
                PONumber = salesOrder.PONumber,
                MessageToClient = string.Format("The original value of Customer Name is {0}", salesOrder.CustomerName),
                ObjectState = ObjectState.Unchanged
            };

            return View(salesOrderViewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel
            {
                SalesOrderId = salesOrder.SalesOrderId,
                CustomerName = salesOrder.CustomerName,
                PONumber = salesOrder.PONumber,
                MessageToClient = string.Format("You are about to delete this sales order,  {0}", salesOrder.CustomerName),
                ObjectState = ObjectState.Deleted
            };

            return View(salesOrderViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult Save(SalesOrderViewModel salesOrderViewModel)
        {
            //ViewModel is mapped to server side model and save in database. 
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.SalesOrderId = salesOrderViewModel.SalesOrderId;
            salesOrder.CustomerName = salesOrderViewModel.CustomerName;
            salesOrder.PONumber = salesOrderViewModel.PONumber;
            salesOrder.ObjectState = salesOrderViewModel.ObjectState;

            _salesContext.SalesOrders.Attach(salesOrder);
            _salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = Helpers.ConvertState(salesOrder.ObjectState);
            _salesContext.SaveChanges();

            if (salesOrder.ObjectState == ObjectState.Deleted)
            {
                //Return to the view list
                return Json(new { newLocation = "/Sales/Index/" });
            }

            switch (salesOrderViewModel.ObjectState)
            {
                case ObjectState.Unchanged:
                    break;
                case ObjectState.Added:
                    salesOrderViewModel.MessageToClient = string.Format("{0}'s sales order is added", salesOrder.CustomerName);
                    break;
                case ObjectState.Modified:
                    salesOrderViewModel.MessageToClient = string.Format("{0}'s sales order is modified", salesOrder.CustomerName);
                    break;
                default:
                    break;
            }

            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            //Data is updated in the database - client can no start with unchanged data
            salesOrderViewModel.ObjectState = ObjectState.Unchanged;

            //Saved ViewModel data is sent back to Client as ananoumous data
            return Json(new { salesOrderViewModel });
        }
    }
}
