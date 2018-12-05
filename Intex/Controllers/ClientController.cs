using Intex.DAL;
using Intex.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace Intex.Controllers
{
    public class ClientController : Controller
    {
        private IntexContext db = new IntexContext(); 

        // GET: Client
        public ActionResult Index()
        {
            return View(db.samples.ToList());
        }
    }
}