using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.models;
using System.Data.Entity;
using System.Data;
using System.Net;

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
                orderSamples.SampleList = new Dictionary<Samples, List<TestTubes>>();
                foreach (Samples sample in Database.samples.Include(w => w.assay).ToList())
                {
                    if (sample.LTNumber == order.LTNumber)
                    {
                        orderSamples.SampleList.Add(sample, null);
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = Database.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customers customer)
        {
            Database.Entry(customer).State = EntityState.Modified;
            Database.SaveChanges();
            return RedirectToAction("Customers", "Seattle");
        }
    }
}