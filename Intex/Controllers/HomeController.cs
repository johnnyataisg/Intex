using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "Seattle")
            {
                return RedirectToAction("Index", "Seattle");
            }
            else if (username == "Singapore")
            {
                return RedirectToAction("Index", "Singapore");
            }
            else
            {
                return View();
            }
        }
    }
}