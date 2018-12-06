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
            var workOrders = Database.WorkOrders.Include(w => w.compounds).Include(w => w.customers);
            return View(workOrders.ToList());
        }
    }
}