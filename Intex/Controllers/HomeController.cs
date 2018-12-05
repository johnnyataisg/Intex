using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.models;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        private IntexContext database = new IntexContext();

        // GET: Home
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            List<EmployeeUsers> allUsers = database.employeeusers.ToList();
            foreach (EmployeeUsers user in allUsers)
            {
                if (user.Username == username && user.Password == password)
                {
                    Employees employee = (from person in database.employees where person.Username == username select person).FirstOrDefault();
                    string permission = employee.OfficeLocation;
                    if (permission == "Seattle")
                    {
                        return RedirectToAction("Index", "Seattle");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Singapore");
                    }
                }
            }
            return View();
        }
    }
}