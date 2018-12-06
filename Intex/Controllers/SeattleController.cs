using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.models;
using System.Data.Entity;
using System.Data;

namespace Intex.Controllers
{
    public class SeattleController : Controller
    {
        private IntexContext Database = new IntexContext();
        // GET: Seattle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllOrders()
        {
            var workOrders = Database.workorders.Include(w => w.compounds).Include(w => w.customers);
            List<WorkOrders> allOrders = workOrders.ToList();
            List<OrderDetails> allOrdersSamples = new List<OrderDetails>();
            foreach (WorkOrders order in allOrders)
            {
                OrderDetails orderSamples = new OrderDetails();
                orderSamples.WorkOrder = order;
                orderSamples.SampleList = new List<Samples>();
                foreach (Samples sample in Database.samples.Include(w => w.assay).ToList())
                {
                    if (sample.LTNumber == order.LTNumber)
                    {
                        orderSamples.SampleList.Add(sample);
                    }
                }
                allOrdersSamples.Add(orderSamples);
            }
            return View(allOrdersSamples);
        }

        public ActionResult Customers()
        {
            var customers = Database.customers.Include(w => w.customerusers);
            List<Customers> allCustomers = customers.ToList();
            return View(allCustomers);
        }
    }
}