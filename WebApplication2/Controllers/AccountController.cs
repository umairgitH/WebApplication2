using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.BL;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public AccountController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            bool isRegister = _employeeService.RegisterEmployee(user);
            if (isRegister)
            {
                return Json(new { result = "Success" });
            }
            else
            {
                return Json(new { result = "Registration failed" });
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            bool isLogin = _employeeService.GetUser(user);
            if (isLogin)
            {
                return Json(new { result = "Success" });
            }
            else
            {
                return Json(new { result = "Registration failed" });
            }
        }
    }
}