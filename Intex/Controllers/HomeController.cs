using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Intex.DAL;
using Intex.models;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        private IntexContext database = new IntexContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Locations()
        {
            return View();
        }

        // GET: Customers/Create
        [HttpGet]
        public ActionResult NewAccount()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAccount([Bind(Include = "CustomerID,CustomerName,PhoneNumber,Email,Address,City,Zip,State,Country,PaymentInfo,TotalOrderVolume,DiscountQualify,Username")] Customers customers,[Bind(Include ="Username,Password")] CustomerUsers customerusers)
        {
            if (ModelState.IsValid)
            {
                database.customers.Add(customers);
                customerusers.Username = customers.Username;
                database.customerusers.Add(customerusers);
                database.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.Username = new SelectList(database.customerusers, "Username", "Password", customers.Username);
            return View(customers);
        }

        // GET: Home
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            List<CustomerUsers> allUsers = database.customerusers.ToList();
            foreach (CustomerUsers user in allUsers)
            {
                if (user.Username == username && user.Password == password)
                {
                    FormsAuthentication.SetAuthCookie(username, true);
                    return RedirectToAction("Index", "Customers", new { id = username });
                }
            }
            ViewBag.ValidationMessage = "<p style=\"color: red;\">Username or password is wrong</p>";
            return View();
        }
    }
}