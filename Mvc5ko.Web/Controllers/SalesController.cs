﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mvc5ko.DataLayer;
using Mvc5ko.Model;
using Mvc5ko.Web.ViewModels;

namespace Mvc5ko.Web.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext _salesContext;

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

            SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            salesOrderViewModel.MessageToClient = "I originated from the viewmodel, rather than the model.";

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

            SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            salesOrderViewModel.MessageToClient = string.Format("The original value of Customer Name is {0}.", salesOrderViewModel.CustomerName);
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

            SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            salesOrderViewModel.MessageToClient = "You are about to permanently delete this sales order.";
            salesOrderViewModel.ObjectState = ObjectState.Deleted;

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
            SalesOrder salesOrder = ViewModels.Helpers.CreateSalesOrderFromSalesOrderViewModel(salesOrderViewModel);

            _salesContext.SalesOrders.Attach(salesOrder);
            if (salesOrder.ObjectState == ObjectState.Deleted)
            {
                foreach (SalesOrderItemViewModel salesOrderItemViewModel in salesOrderViewModel.SalesOrderItems)
                {
                    SalesOrderItem salesOrderItem = _salesContext.SalesOrderItems.Find(salesOrderItemViewModel.SalesOrderItemId);
                    if (salesOrderItem != null)
                        salesOrderItem.ObjectState = ObjectState.Deleted;
                }
            }
            else
            {
                foreach (int salesOrderItemId in salesOrderViewModel.SalesOrderItemsToDelete)
                {
                    SalesOrderItem salesOrderItem = _salesContext.SalesOrderItems.Find(salesOrderItemId);
                    if (salesOrderItem != null)
                        salesOrderItem.ObjectState = ObjectState.Deleted;
                }
            }

            _salesContext.ApplyStateChanges();
            //_salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = DataLayer.Helpers.ConvertState(salesOrder.ObjectState);
            _salesContext.SaveChanges();

            if (salesOrder.ObjectState == ObjectState.Deleted)
                return Json(new { newLocation = "/Sales/Index/" }); //Return to the view list

            //salesOrderViewModel.MessageToClient = ViewModels.Helpers.GetMessageToClient(salesOrderViewModel.ObjectState, salesOrder.CustomerName);
            //salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            //salesOrderViewModel.ObjectState = ObjectState.Unchanged; //Data is updated in the database - client can no start with unchanged data

            string messageToClient = ViewModels.Helpers.GetMessageToClient(salesOrderViewModel.ObjectState, salesOrder.CustomerName);
            salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            salesOrderViewModel.MessageToClient = messageToClient;

            //Saved ViewModel data is sent back to Client as ananoumous data
            return Json(new { salesOrderViewModel });
        }
    }
}
