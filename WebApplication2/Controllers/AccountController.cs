using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.Enum;
using EmployeeTrainningClassLibrary.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _employeeService;
        public AccountController(IUserService employeeService)
        {
            _employeeService = employeeService;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            bool isRegister = _employeeService.IsEmployeeRegistered(user);
            bool isEmailUnique =_employeeService.IsEmailUnique(user.Email);

            List<string> errorMessages = new List<string>();
            if (!isEmailUnique)
            {
                errorMessages.Add("emailNotUnique");
            }
            if (errorMessages.Count > 0)
            {
                return Json(new {result ="Registration failed", errors = errorMessages});
            }
            else
            {
                return Json(new { result = "Success" });
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            //extracting the tuple here.
            (bool isAuthenticated, int userId, int roleId) = _employeeService.IsUserExists(user);
            if (isAuthenticated)
            {
                // Store UserId and RoleId in session
                Session["UserId"] = userId;
                Session["RoleId"] = roleId;

                RoleEnum roleEnum = (RoleEnum)roleId;
                Session["CurrentRoleName"] = Enum.GetName(typeof(RoleEnum), roleEnum);

                return Json(new { result = "Success" });
            }
            else
            {
                return Json(new { result = "Login failed" });
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogOut() {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}
