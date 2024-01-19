using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult AccessDenied()
        {
            ViewBag.Message = " Access Denied";
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error500(string errorMsg)
        {
            ViewBag.Exception = errorMsg;
            return View();
        }
        public ActionResult DefaultError()
        {
            return View();
        }
    }
}