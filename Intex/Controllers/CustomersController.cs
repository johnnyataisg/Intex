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

        public ActionResult WorkOrders1()
        {
            WorkOrders a = new WorkOrders();
            a.customers = db.customers.Find(userid);
            a.compounds = new Compounds();
            if (a == null)
            {
                return HttpNotFound();
            }
            NumberInput ni = new NumberInput();
            ni.wo = a;
            return View(ni);
        }

        public ActionResult MyWorkOrders()
        {
            return View(db.workorders.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkOrders1([Bind(Include ="wo,input")] NumberInput i)
        {
            if (ModelState.IsValid)
            {
                i.wo.CustomerID = userid;
                i.wo.DateDue = DateTime.Now.AddDays(5);
                i.wo.ReceiveDate = DateTime.Now;
                db.workorders.Add(i.wo);
                db.SaveChanges();
                int c = i.Input;
                return RedirectToAction("WorkOrders2", new { count = c, lt = i.wo.LTNumber});
            }
            return View(i.wo);
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings([Bind(Include = "CustomerID,CustomerName,PhoneNumber,Email,Address,City,Zip,State,Country,PaymentInfo,TotalOrderVolume,DiscountQualify,Username")] Customers customers, [Bind(Include = "Username,Password")] CustomerUsers customerusers)
        {
            if (ModelState.IsValid)
            {
                db.customers.Find(userid).CustomerName = customers.CustomerName;
                db.customers.Find(userid).PhoneNumber = customers.PhoneNumber;
                db.customers.Find(userid).Email = customers.Email;
                db.customers.Find(userid).Address = customers.Address;
                db.customers.Find(userid).City = customers.City;
                db.customers.Find(userid).Zip = customers.Zip;
                db.customers.Find(userid).State = customers.State;
                db.customers.Find(userid).Country = customers.Country;
                db.customers.Find(userid).PaymentInfo = customers.PaymentInfo;
                db.customers.Find(userid).customerusers.Password = customerusers.Password;
                db.SaveChanges();
                return RedirectToAction("Login","Home");
            }

            return View(customers);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.customers.Find(id);
            db.customers.Remove(customers);
            db.SaveChanges();
            return RedirectToAction("Index");
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
