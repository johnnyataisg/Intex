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
    public class SingaporeController : Controller
    {
        private IntexContext Database = new IntexContext();

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
                        List<TestTubes> allTubes = new List<TestTubes>();
                        foreach (TestTubes tube in Database.testtubes.Include(w => w.test).ToList())
                        {
                            if (tube.SampleID == sample.SampleID)
                            {
                                allTubes.Add(tube);
                            }
                        }
                        orderSamples.SampleList.Add(sample, allTubes);
                    }
                }
                allOrdersSamples.Add(orderSamples);
            }
            return View(allOrdersSamples);
        }

        public ActionResult InTesting()
        {
            List<WorkOrders> allOrders = Database.workorders.Include(w => w.compounds).Include(w => w.customers).ToList();
            List<OrderDetails> allOrdersSamples = new List<OrderDetails>();
            foreach (WorkOrders order in allOrders)
            {
                List<Samples> allSamples = Database.samples.Where(sample => sample.LTNumber == order.LTNumber && sample.AssayFinished == false).ToList();
                if (allSamples.Count() > 0)
                {
                    OrderDetails orderSamples = new OrderDetails();
                    orderSamples.WorkOrder = order;
                    orderSamples.SampleList = new Dictionary<Samples, List<TestTubes>>();
                    foreach (Samples sample in Database.samples.Include(w => w.assay).ToList())
                    {
                        if (sample.LTNumber == order.LTNumber)
                        {
                            List<TestTubes> allTubes = new List<TestTubes>();
                            foreach (TestTubes tube in Database.testtubes.Include(w => w.test).ToList())
                            {
                                if (tube.SampleID == sample.SampleID)
                                {
                                    allTubes.Add(tube);
                                }
                            }
                            orderSamples.SampleList.Add(sample, allTubes);
                        }
                    }
                    allOrdersSamples.Add(orderSamples);
                }
                else
                {
                    continue;
                }
            }
            return View(allOrdersSamples);
        }

        public ActionResult Completed()
        {
            List<WorkOrders> allOrders = Database.workorders.Include(w => w.compounds).Include(w => w.customers).ToList();
            List<OrderDetails> allOrdersSamples = new List<OrderDetails>();
            foreach (WorkOrders order in allOrders)
            {
                List<Samples> allSamples = Database.samples.Where(sample => sample.LTNumber == order.LTNumber && sample.AssayFinished == false).ToList();
                if (allSamples.Count() == 0)
                {
                    OrderDetails orderSamples = new OrderDetails();
                    orderSamples.WorkOrder = order;
                    orderSamples.SampleList = new Dictionary<Samples, List<TestTubes>>();
                    foreach (Samples sample in Database.samples.Include(w => w.assay).ToList())
                    {
                        if (sample.LTNumber == order.LTNumber)
                        {
                            List<TestTubes> allTubes = new List<TestTubes>();
                            foreach (TestTubes tube in Database.testtubes.Include(w => w.test).ToList())
                            {
                                if (tube.SampleID == sample.SampleID)
                                {
                                    allTubes.Add(tube);
                                }
                            }
                            orderSamples.SampleList.Add(sample, allTubes);
                        }
                    }
                    allOrdersSamples.Add(orderSamples);
                }
                else
                {
                    continue;
                }
            }
            return View(allOrdersSamples);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrders order = Database.workorders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompoundID = new SelectList(Database.compounds, "CompoundID", "CompoundName", order.CompoundID);
            ViewBag.CustomerID = new SelectList(Database.customers, "CustomerID", "CustomerName", order.CustomerID);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(WorkOrders workorder)
        {
            Database.Entry(workorder).State = EntityState.Modified;
            Database.SaveChanges();
            return RedirectToAction("AllOrders", "Singapore");
        }

        [HttpGet]
        public ActionResult EditSample(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samples sample = Database.samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayID = new SelectList(Database.assays, "AssayID", "AssayName", sample.AssayID);
            return View(sample);
        }

        [HttpPost]
        public ActionResult EditSample(Samples sample)
        {
            Database.Entry(sample).State = EntityState.Modified;
            Database.SaveChanges();
            return RedirectToAction("AllOrders", "Singapore");
        }
    }
}