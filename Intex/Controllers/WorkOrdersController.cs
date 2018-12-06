using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.models;

namespace Intex.Controllers
{
    public class WorkOrdersController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: WorkOrders
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.compounds).Include(w => w.customers);
            return View(workOrders.ToList());
        }

        // GET: WorkOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrders workOrders = db.WorkOrders.Find(id);
            if (workOrders == null)
            {
                return HttpNotFound();
            }
            return View(workOrders);
        }

        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LTNumber,ReceiveDate,DateDue,ConfirmDateTime,Comments,HasDiscount,CustomerID,CompoundID")] WorkOrders workOrders)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName", workOrders.CompoundID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", workOrders.CustomerID);
            return View(workOrders);
        }

        // GET: WorkOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrders workOrders = db.WorkOrders.Find(id);
            if (workOrders == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName", workOrders.CompoundID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", workOrders.CustomerID);
            return View(workOrders);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LTNumber,ReceiveDate,DateDue,ConfirmDateTime,Comments,HasDiscount,CustomerID,CompoundID")] WorkOrders workOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName", workOrders.CompoundID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", workOrders.CustomerID);
            return View(workOrders);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrders workOrders = db.WorkOrders.Find(id);
            if (workOrders == null)
            {
                return HttpNotFound();
            }
            return View(workOrders);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrders workOrders = db.WorkOrders.Find(id);
            db.WorkOrders.Remove(workOrders);
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
