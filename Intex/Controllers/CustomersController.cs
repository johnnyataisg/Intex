using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Intex.DAL;
using Intex.models;

namespace Intex.Controllers
{
    public class CustomersController : Controller
    {
        private IntexContext db = new IntexContext();
        private static int userid = 0;

        // GET: Customers
        public ActionResult Index(string id)
        {
            Customers customer = db.customers.Where(person => person.Username == id).FirstOrDefault();
            userid = customer.CustomerID;
            return View(customer);
        }

        public ActionResult Support()
        {
            Customers customers = db.customers.Find(userid);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.customerusers, "Username", "Password");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,PhoneNumber,Email,Address,City,Zip,State,Country,PaymentInfo,TotalOrderVolume,DiscountQualify,Username")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        public ActionResult RequestQuote()
        {
            Customers customers = db.customers.Find(userid);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        public ActionResult Invoices()
        {
            Customers customers = db.customers.Find(userid);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        public ActionResult MyWorkOrders()
        {
            Customers customer = db.customers.Find(userid);
            var workorders = db.workorders.Where(order => order.CustomerID == customer.CustomerID);
            List<WorkOrders> myOrders = workorders.ToList();
            List<OrderDetails> allOrdersSamples = new List<OrderDetails>();
            foreach (WorkOrders order in myOrders)
            {
                OrderDetails orderSamples = new OrderDetails();
                orderSamples.WorkOrder = order;
                orderSamples.SampleList = new Dictionary<Samples, List<TestTubes>>();
                foreach (Samples sample in db.samples.Include(w => w.assay).ToList())
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

        public ActionResult NewOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(NewOrder newOrder)
        {
            if (ModelState.IsValid)
            {
                Customers customer = db.customers.Find(userid);

                Compounds newCompound = new Compounds();
                newCompound.CompoundName = newOrder.CompoundName;
                db.compounds.Add(newCompound);
                db.SaveChanges();

                WorkOrders newWorkOrder = new WorkOrders();
                Compounds compound = db.compounds.Where(temp => temp.CompoundName == newOrder.CompoundName).FirstOrDefault();
                newWorkOrder.CompoundID = compound.CompoundID;
                newWorkOrder.CustomerID = userid;
                newWorkOrder.ReceiveDate = DateTime.Now;
                newWorkOrder.DateDue = DateTime.Now.AddDays(5);
                newWorkOrder.Comments = newOrder.Comments;
                if (customer.DiscountQualify == true)
                {
                    newWorkOrder.HasDiscount = true;
                }
                else
                {
                    newWorkOrder.HasDiscount = false;
                }
                db.workorders.Add(newWorkOrder);
                db.SaveChanges();
                
                for (int i = 0; i < newOrder.NumberSamples; i++)
                {
                    Samples newSample = new Samples();
                    newSample.LTNumber = newWorkOrder.LTNumber;
                    newSample.SequenceNumber = i + 1;
                    newSample.SampleID = "LT" + newSample.LTNumber + "-" + newSample.SequenceNumber;
                    newSample.AssayFinished = false;
                    db.samples.Add(newSample);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { id = customer.Username });
            }
            return View();
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samples sample = db.samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        public ActionResult LogOut()
        {
            userid = 0;
            return RedirectToAction("Index", "Home");
        }

        // GET: Customers/Edit/5
        [HttpGet]
        public ActionResult Settings()
        {

            Customers customers = db.customers.Find(userid);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(Customers customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login","Home");
            }
            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
