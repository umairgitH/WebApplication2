using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.Enum;
using EmployeeTrainningClassLibrary.Models;
using EmployeeTrainningClassLibrary.RoleRedirector;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.Linq;
using System.Threading.Tasks;

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
            List<string> managerNameList = _employeeService.ListOfManagerName();
            ViewBag.managerName = managerNameList;
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                (bool isRegister, List<string> errorMessages) = await _employeeService.IsEmployeeRegisteredAsync(user);
                if (isRegister)
                {
                    return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "Registration failed", errors = errorMessages }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var errors = ModelState.ToDictionary(
                    attribute => attribute.Key,
                    attribute => attribute.Value.Errors.Select(error => error.ErrorMessage).ToArray()
                );
                return Json(new { result = "InvalidInput", errors });
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> Login(LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    attribute => attribute.Key,
                    attribute => attribute.Value.Errors.Select(error => error.ErrorMessage).ToArray()
                );
                return Json(new { result = "InvalidInput", errors });
            }
            //extracting the tuple here.
            (bool isAuthenticated, int userId, int roleId) = await _employeeService.IsUserExistsAsync(user);
            if (isAuthenticated)
            {
                Session["UserId"] = userId;
                Session["RoleId"] = roleId;

                RoleEnum roleEnum = (RoleEnum)roleId;
                Session["CurrentRoleName"] = Enum.GetName(typeof(RoleEnum), roleEnum);

                string roleName = Session["CurrentRoleName"].ToString();
                var redirectInfo = DetermineUserView.GetRedirectionInfo(roleName);

                return Json(new { result = "Success", controller = redirectInfo.controller, action = redirectInfo.action }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Login failed" }, JsonRequestBehavior.AllowGet);
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogOut() {
            //completely end the user's session, requiring them to start a new session on the next request.
            //(clear key and value and also mark the sessionID as invalid)
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}
