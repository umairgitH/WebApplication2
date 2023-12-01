using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.BL;
using WebApplication2.DAL;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataAccessLayer _layer;
        private readonly IEmployeeDAL _employee;
        //private readonly EmployeeDAL _employee;
        //private readonly IEmployeeService employeeService;
        public HomeController(IDataAccessLayer layer, IEmployeeDAL employee)
        {
            _layer = layer;
            _employee = employee;
        }

        public ActionResult Index()
        {
            try
            {
                // Create a new user
                User user = new User()
                {
                    FirstName = "Muhammad Umair",
                    LastName = "Googoolee",
                    Email = "umairgoogoolee@gmail.com",
                    PhoneNum = 57784889,
                    Password = "12344umair",
                    ManagerName = "Dev",
                };

                // Register the employee
                _employee.RegisterEmployee(user);

                // Set ViewBag values
                ViewBag.Message = "Employee registered successfully";
                ViewBag.MyName = "MVC";
            }
            catch (Exception ex)
            {

                ViewBag.Message = $"Error registering employee: {ex.Message}";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}